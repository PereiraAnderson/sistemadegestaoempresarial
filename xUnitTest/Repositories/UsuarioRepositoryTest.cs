using Xunit;
using SGE.Context.Models;
using SGE.Context.Repositories;
using System.Linq;
using System.Collections.Generic;
using SGE.Infra.Enums;

namespace SGE.Test.Repositories
{
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
                Nome = "Usuário 3",
                CPF = "33333333333",
                Login = "usuario3",
                Perfil = EnumUsuarioPerfil.FUNCIONARIO,
                Senha = "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS"
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
            Assert.NotEmpty(result);
        }

        [Fact]
        public void UsuarioRepositoryGetByIDTest()
        {
            var id = 1L;
            var includes = Enumerable.Empty<string>();

            var result = _usuarioRepository.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void UsuarioRepositoryUpdateTest()
        {
            var id = 1L;
            var includes = Enumerable.Empty<string>();
            var usuario = _usuarioRepository.Get(id, includes);
            usuario.Ativo = false;

            var result = _usuarioRepository.Update(usuario);
            var count = _usuarioRepository.SaveChanges();

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.Equal(1, count);
            Assert.False(result.Ativo);
        }

        [Fact]
        public void UsuarioRepositoryRemoveByIDTest()
        {
            var id = 1L;
            var result = _usuarioRepository.Remove(id);
            var count = _usuarioRepository.SaveChanges();

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.Equal(1, count);
            Assert.False(result.Ativo);
        }
    }
}