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
    public class UsuarioControllerTest
    {
        public bool UsuarioServiceCreated;

        private UsuarioController _usuarioController { get; set; }

        public UsuarioControllerTest()
        {
            var usuarioService = CreateUsuarioService();
            _usuarioController = new UsuarioController(null, usuarioService, null);
        }

        [Fact]
        public void UsuarioControllerPostCreatedTest()
        {
            var usuario = new UsuarioView
            {
                Perfil = EnumUsuarioPerfil.FUNCIONARIO,
                Nome = "Usuário Teste",
                CPF = "111.111.111-11",
                Senha = "123456"
            };

            var result = _usuarioController.PostPonto(usuario);

            Assert.NotNull(result);
            Assert.True(result is CreatedAtActionResult);

            var createdResponse = result as CreatedAtActionResult;

            Assert.IsType<UsuarioView>(createdResponse.Value);
            Assert.Equal(StatusCodes.Status201Created, createdResponse.StatusCode);

            Assert.True(UsuarioServiceCreated);
        }

        [Fact]
        public void UsuarioControllerGetUsuarioOkTest()
        {
            var id = 1;
            var includes = Enumerable.Empty<string>();

            var result = _usuarioController.GetUsuario(id, includes);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<UsuarioView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }

        [Fact]
        public void UsuarioControllerGetUsuariosOkTest()
        {
            var paginacao = new Paginacao
            {
                Pagina = 1,
                ListaTodos = false,
                Tamanho = 10
            };
            var filtro = new UsuarioFiltro
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

            var result = _usuarioController.GetUsuarios(paginacao, filtro, ordenacao);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<Paginacao<UsuarioView>>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }

        [Fact]
        public void UsuarioControllerPutOkTest()
        {
            var usuario = new UsuarioView
            {
                Perfil = EnumUsuarioPerfil.FUNCIONARIO,
                Nome = "Usuário Teste",
                CPF = "111.111.111-11",
                Senha = "123456"
            };

            var result = _usuarioController.PutUsuario(usuario);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<UsuarioView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }

        [Fact]
        public void UsuarioControllerPutAlteraSenhaOkTest()
        {
            var alterarSenha = new AlterarSenha
            {
                Login = "",
                SenhaAtual = "",
                SenhaNova = ""
            };

            var result = _usuarioController.PutUsuarioAlteraSenha(alterarSenha);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<UsuarioView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }

        [Fact]
        public void UsuarioControllerDeleteOkTest()
        {
            var id = 0;

            var result = _usuarioController.DeleteUsuario(id);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<UsuarioView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }

        [Fact]
        public void UsuarioControlleLoginOkTest()
        {
            var longinIn = new LoginIn
            {
                Login = "",
                Senha = ""
            };

            var result = _usuarioController.Login(longinIn);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<LoginOut>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }



        #region Mocks

        public IUsuarioService CreateUsuarioService()
        {
            var usuarioService = new Mock<IUsuarioService>();
            usuarioService
                .Setup(x => x.Add(It.IsAny<Usuario>()))
                .Callback((Usuario usuario) =>
                    UsuarioServiceCreated = true
                )
                .Returns((Usuario usuario) =>
                    new Usuario()
                );

            usuarioService
                .Setup(x => x.Update(It.IsAny<Usuario>()))
                .Returns((Usuario usuario) =>
                    new Usuario()
                );

            usuarioService
                .Setup(x => x.Get(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
                .Returns((long id, IEnumerable<string> includes) =>
                    new Usuario()
                );

            usuarioService
                .Setup(x => x.Get(It.IsAny<Paginacao>(), It.IsAny<UsuarioFiltro>(), It.IsAny<Ordenacao>()))
                .Returns((Paginacao paginacao, UsuarioFiltro filtro, Ordenacao ordenacao) =>
                    new Paginacao<Usuario>()
                    {
                        ListaItens = Enumerable.Repeat(new Usuario(), 1),
                        NumeroPagina = 1,
                        TamanhoPagina = 10,
                        TotalItens = 2,
                        TotalPaginas = 1
                    }
                );

            usuarioService
                .Setup(x => x.Remove(It.IsAny<long>()))
                .Returns((long id) =>
                    new Usuario()
                );

            usuarioService
                .Setup(x => x.AlterarSenha(It.IsAny<AlterarSenha>()))
                .Returns((AlterarSenha alterarSenha) =>
                    new Usuario()
                );

            usuarioService
                .Setup(x => x.Login(It.IsAny<LoginIn>()))
                .Returns((LoginIn loginIn) =>
                    new LoginOut()
                );

            return usuarioService.Object;
        }

        #endregion Mocks
    }
}
