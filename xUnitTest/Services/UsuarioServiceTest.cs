using System;
using Xunit;
using SGE.Context.Models;
using SGE.Infra.Enums;
using SGE.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SGE.Context.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using SGE.Services.Interfaces;

namespace SGE.Test.Services
{
    public class UsuarioServiceTest
    {
        private bool UsuarioRepositoryCreated;

        // private bool PontoServiceCreated;

        private UsuarioService _usuarioService { get; set; }

        public UsuarioServiceTest()
        {
            var usuarioRepository = CreateUsuarioRepository();
            var pontoService = CreatePontoService();
            var userManager = CreateUserManager();
            var signInManager = CreateSignInManager();
            IServiceProvider serviceProvider = null;
            IConfiguration configuration = null;

            _usuarioService = new UsuarioService(usuarioRepository, pontoService,
                userManager, signInManager, serviceProvider, configuration);
        }

        [Fact]
        public void UsuarioServiceGetOkTest()
        {
            var id = Guid.NewGuid().ToString();
            var includes = Enumerable.Empty<string>();

            var result = _usuarioService.Get(id, includes);

            Assert.NotNull(result);
            Assert.True(result is Usuario);

            Assert.False(UsuarioRepositoryCreated);
        }

        #region Mocks

        public IPontoService CreatePontoService()
        {
            /*var pontoService = new Mock<IPontoService>();
            pontoService
                .Setup(x => x.Add(It.IsAny<Ponto>()))
                .Callback((Ponto ponto) =>
                    PontoServiceCreated = true
                )
                .Returns((Ponto ponto) =>
                    new Ponto()
                );

            pontoService
                .Setup(x => x.Get(It.IsAny<long>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string id, IEnumerable<string> includes) =>
                    new Ponto()
                );

            return pontoService.Object;*/
            return null;
        }

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
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string id, IEnumerable<string> includes) =>
                    new Usuario()
                );

            return usuarioRepository.Object;
        }

        public UserManager<Usuario> CreateUserManager()
        {
            // var userManager = new Mock<UserManager<Usuario>>();

            // userManager
            //     .Setup(x => x.CreateAsync(It.IsAny<Usuario>()))
            //     .Callback((Usuario usuario) =>
            //         UsuarioRepositoryCreated = true
            //     )
            //     .ReturnsAsync((Usuario usuario) =>
            //         new IdentityResult()
            //     );

            // return userManager.Object;

            return null;
        }

        public SignInManager<Usuario> CreateSignInManager()
        {
            // var signInManager = new Mock<SignInManager<Usuario>>();

            // return signInManager.Object;
            return null;
        }

        #endregion Mocks
    }
}