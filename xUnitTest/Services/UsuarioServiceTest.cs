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
    public class UsuarioServiceTest
    {
        private bool UsuarioRepositoryCreated;

        private UsuarioService _usuarioService { get; set; }

        public UsuarioServiceTest()
        {
            var usuarioRepository = CreateUsuarioRepository();

            _usuarioService = new UsuarioService(usuarioRepository);
        }

        [Fact]
        public void UsuarioServiceGetByIdTest()
        {
            var id = 0;
            var includes = Enumerable.Empty<string>();

            var result = _usuarioService.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.False(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceGetByCpfTest()
        {
            var cpf = "00000000000";
            var includes = Enumerable.Empty<string>();

            var result = _usuarioService.GetByCPF(cpf, includes);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.False(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceGetByLoginTest()
        {
            var login = "";
            var includes = Enumerable.Empty<string>();

            var result = _usuarioService.GetByLogin(login, includes);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.False(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceGetListTest()
        {
            var paginacao = new Paginacao();
            var usuarioFiltro = new UsuarioFiltro();
            var ordenacao = new Ordenacao();

            var result = _usuarioService.Get(paginacao, usuarioFiltro, ordenacao);

            Assert.NotNull(result);
            Assert.True(result is Paginacao<Usuario>);
            Assert.False(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceAddTest()
        {
            var usuario = new Usuario
            {
                Senha = "123456"
            };

            var result = _usuarioService.Add(usuario);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.True(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceUpdateTest()
        {
            var usuario = new Usuario();

            var result = _usuarioService.Update(usuario);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.False(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceRemoveByIdTest()
        {
            var id = 0;

            var result = _usuarioService.Remove(id);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.False(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceLoginTest()
        {
            var loginIn = new LoginIn
            {
                Senha = "123456"
            };

            var result = _usuarioService.Login(loginIn);

            Assert.NotNull(result);
            Assert.True(result is LoginOut);
            Assert.False(UsuarioRepositoryCreated);
        }

        [Fact]
        public void UsuarioServiceAlterarSenhaTest()
        {
            var alterarSenha = new AlterarSenha
            {
                Login = string.Empty,
                SenhaAtual = "123456",
                SenhaNova = "123456"
            };

            var result = _usuarioService.AlterarSenha(alterarSenha);

            Assert.NotNull(result);
            Assert.True(result is Usuario);
            Assert.False(UsuarioRepositoryCreated);
        }

        #region Mocks

        public IUsuarioRepository CreateUsuarioRepository()
        {
            var usuarioRepository = new Mock<IUsuarioRepository>();
            usuarioRepository
                .Setup(x => x.Add(It.IsAny<Usuario>()))
                .Callback((Usuario usuario) =>
                    UsuarioRepositoryCreated = true
                )
                .Returns((Usuario usuario) =>
                    new Usuario()
                );

            usuarioRepository
                .Setup(x => x.Update(It.IsAny<Usuario>()))
                .Returns((Usuario usuario) =>
                    new Usuario()
                );

            usuarioRepository
                .Setup(x => x.Remove(It.IsAny<long>()))
                .Returns((long id) =>
                    new Usuario()
                );

            usuarioRepository
                .Setup(x => x.Get(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
                .Returns((long id, IEnumerable<string> includes) =>
                    new Usuario()
                );

            usuarioRepository
                .Setup(x => x.GetByCPF(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string cpf, IEnumerable<string> includes) =>
                    new Usuario()
                );

            usuarioRepository
                .Setup(x => x.GetByLogin(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string login, IEnumerable<string> includes) =>
                    new Usuario
                    {
                        Senha = "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS"
                    }
                );

            usuarioRepository
                .Setup(x => x.Get(It.IsAny<Paginacao>(), It.IsAny<UsuarioFiltro>(), It.IsAny<Ordenacao>()))
                .Returns((Paginacao paginacao, UsuarioFiltro usuarioFiltro, Ordenacao ordenacao) =>
                    new Paginacao<Usuario>
                    {
                        ListaItens = Enumerable.Repeat(new Usuario(), 1),
                        NumeroPagina = 1,
                        TamanhoPagina = 10,
                        TotalItens = 2,
                        TotalPaginas = 1
                    }
                );

            return usuarioRepository.Object;
        }

        #endregion Mocks
    }
}