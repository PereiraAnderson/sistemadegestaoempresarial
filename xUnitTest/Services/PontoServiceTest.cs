using Xunit;
using SGE.Context.Models;
using SGE.Services;
using System.Linq;
using SGE.Context.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using SGE.Infra.Utils;
using SGE.Infra.Filters;
using SGE.Infra.Views;

namespace SGE.Test.Services
{
    public class PontoServiceTest
    {
        private bool PontoRepositoryCreated;

        private PontoService _pontoService { get; set; }

        public PontoServiceTest()
        {
            var pontoRepository = CreatePontoRepository();

            _pontoService = new PontoService(pontoRepository);
        }

        [Fact]
        public void PontoServiceGetByIdTest()
        {
            var id = 0;
            var includes = Enumerable.Empty<string>();

            var result = _pontoService.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
            Assert.False(PontoRepositoryCreated);
        }
/*
        [Fact]
        public void PontoServiceGetByCpfTest()
        {
            var cpf = "00000000000";
            var includes = Enumerable.Empty<string>();

            var result = _pontoService.GetByCPF(cpf, includes);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
            Assert.False(PontoRepositoryCreated);
        }

        [Fact]
        public void PontoServiceGetByLoginTest()
        {
            var login = "";
            var includes = Enumerable.Empty<string>();

            var result = _pontoService.GetByLogin(login, includes);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
            Assert.False(PontoRepositoryCreated);
        }
*/
        [Fact]
        public void PontoServiceGetListTest()
        {
            var paginacao = new Paginacao();
            var pontoFiltro = new PontoFiltro();
            var ordenacao = new Ordenacao();

            var result = _pontoService.Get(paginacao, pontoFiltro, ordenacao);

            Assert.NotNull(result);
            Assert.True(result is Paginacao<Ponto>);
            Assert.False(PontoRepositoryCreated);
        }

        [Fact]
        public void PontoServiceAddTest()
        {
            var ponto = new Ponto
            {
                Data = System.DateTimeOffset.Now
            };

            var result = _pontoService.Add(ponto);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
            Assert.True(PontoRepositoryCreated);
        }

        [Fact]
        public void PontoServiceUpdateTest()
        {
            var ponto = new Ponto();

            var result = _pontoService.Update(ponto);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
            Assert.False(PontoRepositoryCreated);
        }

        [Fact]
        public void PontoServiceRemoveByIdTest()
        {
            var id = 0;

            var result = _pontoService.Remove(id);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
            Assert.False(PontoRepositoryCreated);
        }

        #region Mocks

        public IPontoRepository CreatePontoRepository()
        {
            var pontoRepository = new Mock<IPontoRepository>();
            pontoRepository
                .Setup(x => x.Add(It.IsAny<Ponto>()))
                .Callback((Ponto ponto) =>
                    PontoRepositoryCreated = true
                )
                .Returns((Ponto ponto) =>
                    new Ponto()
                );

            pontoRepository
                .Setup(x => x.Update(It.IsAny<Ponto>()))
                .Returns((Ponto ponto) =>
                    new Ponto()
                );

            pontoRepository
                .Setup(x => x.Remove(It.IsAny<long>()))
                .Returns((long id) =>
                    new Ponto()
                );

            pontoRepository
                .Setup(x => x.Get(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
                .Returns((long id, IEnumerable<string> includes) =>
                    new Ponto()
                );
/*
            pontoRepository
                .Setup(x => x.GetByCPF(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string cpf, IEnumerable<string> includes) =>
                    new Ponto()
                );

            pontoRepository
                .Setup(x => x.GetByLogin(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string login, IEnumerable<string> includes) =>
                    new Ponto
                    {
                        Senha = "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS"
                    }
                );
*/
            pontoRepository
                .Setup(x => x.Get(It.IsAny<Paginacao>(), It.IsAny<PontoFiltro>(), It.IsAny<Ordenacao>()))
                .Returns((Paginacao paginacao, PontoFiltro pontoFiltro, Ordenacao ordenacao) =>
                    new Paginacao<Ponto>
                    {
                        ListaItens = Enumerable.Repeat(new Ponto(), 1),
                        NumeroPagina = 1,
                        TamanhoPagina = 10,
                        TotalItens = 2,
                        TotalPaginas = 1
                    }
                );

            return pontoRepository.Object;
        }

        #endregion Mocks
    }
}