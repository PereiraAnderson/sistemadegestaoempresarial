using System;
using Xunit;
using SGE.Controllers;
using SGE.Infra.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SGE.Infra.Enums;
using SGE.Infra.Views;
using System.Linq;
using SGE.Test.Services;
using SGE.Services.Interfaces;
using Moq;
using SGE.Context.Models;
using System.Collections.Generic;

namespace SGE.Test.Controllers
{
    public class UsuarioServiceTest
    {
        public bool UsuarioServiceCreated;

        private UsuarioController _usuarioController { get; set; }

        public UsuarioServiceTest()
        {
            var usuarioService = CreateUsuarioService();
            _usuarioController = new UsuarioController(null, usuarioService, null);
        }

        [Fact]
        public async void UsuarioControllerPostBadRequestTest()
        {
            var usuario = new UsuarioView();
            var result = await _usuarioController.PostUsuarios(usuario);

            Assert.NotNull(result);
            Assert.True(result is BadRequestObjectResult);

            var badResponse = result as BadRequestObjectResult;

            Assert.IsType<Erro>(badResponse.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }

        [Fact]
        public async void UsuarioControllerPostCreatedTest()
        {
            var usuario = new UsuarioView
            {
                Perfil = EnumUsuarioPerfil.FUNCIONARIO,
                Nome = "Usuário Teste",
                CPF = "111.111.111-11",
                Email = "teste@teste.com.br",
                Senha = "123456"
            };

            var result = await _usuarioController.PostUsuarios(usuario);

            Assert.NotNull(result);
            Assert.True(result is CreatedAtActionResult);

            var createdResponse = result as CreatedAtActionResult;

            Assert.IsType<UsuarioView>(createdResponse.Value);
            Assert.Equal(StatusCodes.Status201Created, createdResponse.StatusCode);

            Assert.True(UsuarioServiceCreated);
        }

        [Fact]
        public void UsuarioControllerGetOkTest()
        {
            var id = Guid.NewGuid().ToString();
            var includes = Enumerable.Empty<string>();

            var result = _usuarioController.GetUsuario(id, includes);

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<UsuarioView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);

            Assert.False(UsuarioServiceCreated);
        }


        #region Mocks

        public IUsuarioService CreateUsuarioService()
        {
            var usuarioService = new Mock<IUsuarioService>();
            usuarioService
                .Setup(x => x.Add(It.IsAny<Usuario>(), It.IsAny<EnumUsuarioPerfil>()))
                .Callback((Usuario usuario, EnumUsuarioPerfil perfil) =>
                    UsuarioServiceCreated = true
                )
                .ReturnsAsync((Usuario usuario, EnumUsuarioPerfil perfil) =>
                    new Usuario()
                );

            usuarioService
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string id, IEnumerable<string> includes) =>
                    new Usuario()
                );

            return usuarioService.Object;
        }

        #endregion Mocks
    }
}
