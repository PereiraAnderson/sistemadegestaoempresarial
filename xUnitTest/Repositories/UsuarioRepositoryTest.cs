using Xunit;
using SGE.Context.Models;
using SGE.Context.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace SGE.Test.Repositories
{
    [TestCaseOrderer("SGE.Test.Utils.PriorityOrderer", "UsuarioRepositoryTest")]
    public class UsuarioRepositoryTest
    {
        private UsuarioRepository _usuarioRepository { get; set; }

        public UsuarioRepositoryTest()
        {
            var dbContext = new DbContextTest().DbContext;

            _usuarioRepository = new UsuarioRepository(dbContext);
        }

        [Fact]
        public void UsuarioRepositoryAddOkTest()
        {
            var usuario = new Usuario
            {
                CPF = "111.111.111-11"
            };

            var result = _usuarioRepository.Add(usuario);
            var count = _usuarioRepository.SaveChanges();

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.Equal(1, count);
        }

       [Fact]
        public void UsuarioRepositoryGetOkTest()
        {
            var result = _usuarioRepository.Get().ToList();

            Assert.NotNull(result);
            Assert.True(result is List<Usuario>);
        }

        [Fact]
        public void UsuarioRepositoryGetByIDTest(){
            var id = 0;
            var includes = Enumerable.Empty<string>();

            var result = _usuarioRepository.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
        }

        [Fact]
        public void UsuarioRepositoryUpdateTest(){
            var usuario = new Usuario();

            var result = _usuarioRepository.Update(usuario);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
        }

        [Fact]
        public void UsuarioRepositoryRemoveByIDTest(){
          var id = 0;

            var result = _usuarioRepository.Remove(id);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
        }
    }
}