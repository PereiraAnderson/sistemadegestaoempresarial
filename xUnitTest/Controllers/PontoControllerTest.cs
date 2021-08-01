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
    public class PontoControllerTest
    {
        public bool PontoServiceCreated;

        private PontoController _pontoController { get; set; }

        public PontoControllerTest()
        {
            var pontoService = CreatePontoService();
            _pontoController = new PontoController(null, pontoService);
        }

        [Fact]
        public void PontoControllerPostCreatedTest()
        {
            var ponto = new PontoView
            {
                Data = System.DateTimeOffset.Now,
                
            };

            var result = _pontoController.PostPonto(ponto);

            Assert.NotNull(result);
            Assert.True(result is CreatedAtActionResult);

            var createdResponse = result as CreatedAtActionResult;

            Assert.IsType<PontoView>(createdResponse.Value);
            Assert.Equal(StatusCodes.Status201Created, createdResponse.StatusCode);

            Assert.True(PontoServiceCreated);
        }

        [Fact]
        public void PontoControllerGetPontoOkTest()
        {
            var id = 1;
            var includes = Enumerable.Empty<string>();

            var result = _pontoController.GetPonto(id, includes);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<PontoView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(PontoServiceCreated);
        }

        [Fact]
        public void PontoControllerGetPontosOkTest()
        {
            var paginacao = new Paginacao
            {
                Pagina = 1,
                ListaTodos = false,
                Tamanho = 10
            };
            var filtro = new PontoFiltro
            {
            };
            var ordenacao = new Ordenacao
            {
                OrdenacaoAsc = true,
                OrdenaPor = "Id"
            };

            var result = _pontoController.GetPontos(paginacao, filtro, ordenacao);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<Paginacao<PontoView>>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(PontoServiceCreated);
        }

        [Fact]
        public void PontoControllerPutOkTest()
        {
            var ponto = new PontoView
            {
                Data = System.DateTimeOffset.Now
            };

            var result = _pontoController.PutPonto(ponto);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<PontoView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(PontoServiceCreated);
        }

    
        [Fact]
        public void PontoControllerDeleteOkTest()
        {
            var id = 0;

            var result = _pontoController.DeletePonto(id);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<PontoView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(PontoServiceCreated);
        }

        #region Mocks

        public IPontoService CreatePontoService()
        {
            var pontoService = new Mock<IPontoService>();
            pontoService
                .Setup(x => x.Add(It.IsAny<Ponto>()))
                .Callback((Ponto ponto) =>
                    PontoServiceCreated = true
                )
                .Returns((Ponto ponto) =>
                    new Ponto()
                );

            pontoService
                .Setup(x => x.Update(It.IsAny<Ponto>()))
                .Returns((Ponto ponto) =>
                    new Ponto()
                );

            pontoService
                .Setup(x => x.Get(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
                .Returns((long id, IEnumerable<string> includes) =>
                    new Ponto()
                );

            pontoService
                .Setup(x => x.Get(It.IsAny<Paginacao>(), It.IsAny<PontoFiltro>(), It.IsAny<Ordenacao>()))
                .Returns((Paginacao paginacao, PontoFiltro filtro, Ordenacao ordenacao) =>
                    new Paginacao<Ponto>()
                    {
                        ListaItens = Enumerable.Repeat(new Ponto(), 1),
                        NumeroPagina = 1,
                        TamanhoPagina = 10,
                        TotalItens = 2,
                        TotalPaginas = 1
                    }
                );

            pontoService
                .Setup(x => x.Remove(It.IsAny<long>()))
                .Returns((long id) =>
                    new Ponto()
                );

            return pontoService.Object;
        }

        #endregion Mocks
    }
}
