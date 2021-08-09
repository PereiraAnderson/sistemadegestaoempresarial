using Xunit;
using SGE.Controllers;
using SGE.Infra.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SGE.Infra.Enums;
using System.Linq;
using SGE.Services.Interfaces;
using Moq;
using SGE.Context.Models;
using System.Collections.Generic;
using SGE.Infra.Utils;
using SGE.Infra.Filters;
using SGE.Infra.Views;

namespace SGE.Test.Controllers
{
    public class RequerimentoControllerTest
    {
        public bool RequerimentoServiceCreated;

        private RequerimentoController _requerimentoController { get; set; }

        public RequerimentoControllerTest()
        {
            var requerimentoService = CreateRequerimentoService();
            _requerimentoController = new RequerimentoController(null, requerimentoService, null);
        }

        [Fact]
        public void RequerimentoControllerPostCreatedTest()
        {
            var requerimento = new RequerimentoView
            {
                Perfil = EnumRequerimentoPerfil.FUNCIONARIO,
                Nome = "Usuário Teste",
                CPF = "111.111.111-11",
                Senha = "123456"
            };

            var result = _requerimentoController.PostPonto(requerimento);

            Assert.NotNull(result);
            Assert.True(result is CreatedAtActionResult);

            var createdResponse = result as CreatedAtActionResult;

            Assert.IsType<RequerimentoView>(createdResponse.Value);
            Assert.Equal(StatusCodes.Status201Created, createdResponse.StatusCode);

            Assert.True(RequerimentoServiceCreated);
        }

        [Fact]
        public void RequerimentoControllerGetRequerimentoOkTest()
        {
            var id = 1;
            var includes = Enumerable.Empty<string>();

            var result = _requerimentoController.GetRequerimento(id, includes);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<RequerimentoView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(RequerimentoServiceCreated);
        }

        [Fact]
        public void RequerimentoControllerGetRequerimentosOkTest()
        {
            var paginacao = new Paginacao
            {
                Pagina = 1,
                ListaTodos = false,
                Tamanho = 10
            };
            var filtro = new RequerimentoFiltro
            {
                Ativo = true,
                CPF = "11111111111",
                Includes = Enumerable.Empty<string>(),
                Login = "",
                Nome = ""
            };
            var ordenacao = new Ordenacao
            {
                OrdenacaoAsc = true,
                OrdenaPor = "Id"
            };

            var result = _requerimentoController.GetRequerimentos(paginacao, filtro, ordenacao);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<Paginacao<RequerimentoView>>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(RequerimentoServiceCreated);
        }

        [Fact]
        public void RequerimentoControllerPutOkTest()
        {
            var requerimento = new RequerimentoView
            {
                Perfil = EnumRequerimentoPerfil.FUNCIONARIO,
                Nome = "Usuário Teste",
                CPF = "111.111.111-11",
                Senha = "123456"
            };

            var result = _requerimentoController.PutRequerimento(requerimento);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<RequerimentoView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(RequerimentoServiceCreated);
        }

        [Fact]
        public void RequerimentoControllerPutAlteraSenhaOkTest()
        {
            var alterarSenha = new AlterarSenha
            {
                Login = "",
                SenhaAtual = "",
                SenhaNova = ""
            };

            var result = _requerimentoController.PutRequerimentoAlteraSenha(alterarSenha);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<RequerimentoView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(RequerimentoServiceCreated);
        }

        [Fact]
        public void RequerimentoControllerDeleteOkTest()
        {
            var id = 0;

            var result = _requerimentoController.DeleteRequerimento(id);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<RequerimentoView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(RequerimentoServiceCreated);
        }

        #region Mocks

        public IRequerimentoService CreateRequerimentoService()
        {
            var requerimentoService = new Mock<IRequerimentoService>();
            requerimentoService
                .Setup(x => x.Add(It.IsAny<Requerimento>()))
                .Callback((Requerimento requerimento) =>
                    RequerimentoServiceCreated = true
                )
                .Returns((Requerimento requerimento) =>
                    new Requerimento()
                );

            requerimentoService
                .Setup(x => x.Update(It.IsAny<Requerimento>()))
                .Returns((Requerimento requerimento) =>
                    new Requerimento()
                );

            requerimentoService
                .Setup(x => x.Get(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
                .Returns((long id, IEnumerable<string> includes) =>
                    new Requerimento()
                );

            requerimentoService
                .Setup(x => x.Get(It.IsAny<Paginacao>(), It.IsAny<RequerimentoFiltro>(), It.IsAny<Ordenacao>()))
                .Returns((Paginacao paginacao, RequerimentoFiltro filtro, Ordenacao ordenacao) =>
                    new Paginacao<Requerimento>()
                    {
                        ListaItens = Enumerable.Repeat(new Requerimento(), 1),
                        NumeroPagina = 1,
                        TamanhoPagina = 10,
                        TotalItens = 2,
                        TotalPaginas = 1
                    }
                );

            requerimentoService
                .Setup(x => x.Remove(It.IsAny<long>()))
                .Returns((long id) =>
                    new Requerimento()
                );

            requerimentoService
                .Setup(x => x.AlterarSenha(It.IsAny<AlterarSenha>()))
                .Returns((AlterarSenha alterarSenha) =>
                    new Requerimento()
                );

            requerimentoService
                .Setup(x => x.Login(It.IsAny<LoginIn>()))
                .Returns((LoginIn loginIn) =>
                    new LoginOut()
                );

            return requerimentoService.Object;
        }

        #endregion Mocks
    }
}
