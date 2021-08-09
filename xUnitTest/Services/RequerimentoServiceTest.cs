using Xunit;
using SGE.Context.Models;
using SGE.Services;
using System.Linq;
using SGE.Context.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using SGE.Infra.Utils;
using SGE.Infra.Filters;

namespace SGE.Test.Services
{
    public class RequerimentoServiceTest
    {
        private bool RequerimentoRepositoryCreated;

        private RequerimentoService _requerimentoService { get; set; }

        public RequerimentoServiceTest()
        {
            var requerimentoRepository = CreateRequerimentoRepository();

            _requerimentoService = new RequerimentoService(requerimentoRepository);
        }

        [Fact]
        public void RequerimentoServiceGetByIdTest()
        {
            var id = 0;
            var includes = Enumerable.Empty<string>();

            var result = _requerimentoService.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.False(RequerimentoRepositoryCreated);
        }

        [Fact]
        public void RequerimentoServiceGetListTest()
        {
            var paginacao = new Paginacao();
            var requerimentoFiltro = new RequerimentoFiltro();
            var ordenacao = new Ordenacao();

            var result = _requerimentoService.Get(paginacao, requerimentoFiltro, ordenacao);

            Assert.NotNull(result);
            Assert.True(result is Paginacao<Requerimento>);
            Assert.False(RequerimentoRepositoryCreated);
        }

        [Fact]
        public void RequerimentoServiceAddTest()
        {
            var requerimento = new Requerimento
            {
                Data = System.DateTimeOffset.Now
            };

            var result = _requerimentoService.Add(requerimento);

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.True(RequerimentoRepositoryCreated);
        }

        [Fact]
        public void RequerimentoServiceUpdateTest()
        {
            var requerimento = new Requerimento();

            var result = _requerimentoService.Update(requerimento);

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.False(RequerimentoRepositoryCreated);
        }

        [Fact]
        public void RequerimentoServiceRemoveByIdTest()
        {
            var id = 0;

            var result = _requerimentoService.Remove(id);

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.False(RequerimentoRepositoryCreated);
        }

        #region Mocks

        public IRequerimentoRepository CreateRequerimentoRepository()
        {
            var requerimentoRepository = new Mock<IRequerimentoRepository>();
            requerimentoRepository
                .Setup(x => x.Add(It.IsAny<Requerimento>()))
                .Callback((Requerimento requerimento) =>
                    RequerimentoRepositoryCreated = true
                )
                .Returns((Requerimento requerimento) =>
                    new Requerimento()
                );

            requerimentoRepository
                .Setup(x => x.Update(It.IsAny<Requerimento>()))
                .Returns((Requerimento requerimento) =>
                    new Requerimento()
                );

            requerimentoRepository
                .Setup(x => x.Remove(It.IsAny<long>()))
                .Returns((long id) =>
                    new Requerimento()
                );

            requerimentoRepository
                .Setup(x => x.Get(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
                .Returns((long id, IEnumerable<string> includes) =>
                    new Requerimento()
                );

            requerimentoRepository
                .Setup(x => x.Get(It.IsAny<Paginacao>(), It.IsAny<RequerimentoFiltro>(), It.IsAny<Ordenacao>()))
                .Returns((Paginacao paginacao, RequerimentoFiltro requerimentoFiltro, Ordenacao ordenacao) =>
                    new Paginacao<Requerimento>
                    {
                        ListaItens = Enumerable.Repeat(new Requerimento(), 1),
                        NumeroPagina = 1,
                        TamanhoPagina = 10,
                        TotalItens = 2,
                        TotalPaginas = 1
                    }
                );

            return requerimentoRepository.Object;
        }

        #endregion Mocks
    }
}