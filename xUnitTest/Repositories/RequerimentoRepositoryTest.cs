using Xunit;
using SGE.Context.Models;
using SGE.Context.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace SGE.Test.Repositories
{
    public class RequerimentoRepositoryTest
    {
        private RequerimentoRepository _requerimentoRepository { get; set; }

        public RequerimentoRepositoryTest()
        {
            var dbContext = new DbContextTest().DbContext;

            _requerimentoRepository = new RequerimentoRepository(dbContext);
        }

        [Fact]
        public void RequerimentoRepositoryAddOkTest()
        {
            var requerimento = new Requerimento
            {
                Data = System.DateTimeOffset.Now,
                UsuarioId = 1
            };

            var result = _requerimentoRepository.Add(requerimento);
            var count = _requerimentoRepository.SaveChanges();

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.Equal(1, count);
        }

        [Fact]
        public void RequerimentoRepositoryGetListTest()
        {
            var result = _requerimentoRepository.Get().ToList();

            Assert.NotNull(result);
            Assert.True(result is List<Requerimento>);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void RequerimentoRepositoryGetByID()
        {
            var id = 1L;
            var includes = Enumerable.Empty<string>();

            var result = _requerimentoRepository.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void RequerimentoRepositoryUpdate()
        {
            var id = 1L;
            var includes = Enumerable.Empty<string>();
            var requerimento = _requerimentoRepository.Get(id, includes);
            requerimento.Ativo = false;

            var result = _requerimentoRepository.Update(requerimento);
            var count = _requerimentoRepository.SaveChanges();

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.Equal(1, count);
            Assert.False(result.Ativo);
        }

        [Fact]
        public void RequerimentoRepositoryRemove()
        {
            var id = 1L;
            var result = _requerimentoRepository.Remove(id);
            var count = _requerimentoRepository.SaveChanges();

            Assert.NotNull(result);
            Assert.True(result is Requerimento);
            Assert.Equal(1, count);
            Assert.False(result.Ativo);
        }
    }
}