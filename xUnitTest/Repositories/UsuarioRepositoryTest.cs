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

        [Fact, TestPriority(0)]
        public void UsuarioRepositoryAddOkTest()
        {
            var usuario = new Usuario()
            {
                CPF = "111.111.111-11"
            };

            var result = _usuarioRepository.Add(usuario);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
        }

        [Fact, TestPriority(1)]
        public void UsuarioRepositoryGetOkTest()
        {
            var result = _usuarioRepository.Get().ToList();

            Assert.NotNull(result);
            Assert.True(result is List<Usuario>);
        }
    }
}