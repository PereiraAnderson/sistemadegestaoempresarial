using Xunit;
using SGE.Context.Models;
using SGE.Context.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace SGE.Test.Repositories
{
    [TestCaseOrderer("SGE.Test.Utils.PriorityOrderer", "PontoRepositoryTest")]
    public class PontoRepositoryTest
    {
        private PontoRepository _pontoRepository { get; set; }

        public PontoRepositoryTest()
        {
            var dbContext = new DbContextTest().DbContext;

            _pontoRepository = new PontoRepository(dbContext);
        }

        [Fact]
        public void PontoRepositoryAddOkTest()
        {
            var ponto = new Ponto
            {
                Data = System.DateTimeOffset.Now,
                UsuarioId = 1
            };

            var result = _pontoRepository.Add(ponto);
            var count = _pontoRepository.SaveChanges();

            Assert.NotNull(result);
            Assert.True(result is Ponto);
            Assert.Equal(1, count);
        }

       [Fact]
        public void PontoRepositoryGetListTest()
        {
            var result = _pontoRepository.Get().ToList();

            Assert.NotNull(result);
            Assert.True(result is List<Ponto>);
        }

        [Fact]
        public void PontoRepositoryGetByID(){
            var id = 0;
            var includes = Enumerable.Empty<string>();

            var result = _pontoRepository.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
        }
        [Fact]
        public void PontoRepositoryUpdate(){
            var ponto = new Ponto();

            var result = _pontoRepository.Update(ponto);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
        }
        [Fact]
        public void PontoRepositoryRemove(){
          var id = 0;

            var result = _pontoRepository.Remove(id);

            Assert.NotNull(result);
            Assert.True(result is Ponto);
        }
    }
}