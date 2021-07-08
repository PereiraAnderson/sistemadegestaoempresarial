using System;
using Xunit;
using SGE.Controllers;
using SGE.Infra.Views.Models;
using SGE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SGE.Context.Models;
using SGE.Infra.Enums;

namespace SGE.Test
{
    public class UsuarioControllerTest
    {
        private UsuarioController _usuarioController { get; set; }

        private bool _createdAsync;

        public UsuarioControllerTest()
        {
            var usuarioService = CreateUsuarioService();
            _usuarioController = new UsuarioController(null, usuarioService, null);
        }

        [Fact]
        public async void UsuarioPostBadRequestTest()
        {
            var usuario = new UsuarioView();
            var result = await _usuarioController.PostUsuarios(usuario);
            var badResponse = result as BadRequestObjectResult;

            Assert.NotNull(badResponse);
            Assert.Equal(400, badResponse.StatusCode);
        }

        [Fact]
        public async void UsuarioPostOkTest()
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
            Assert.True(result is OkObjectResult);

            var okResponse = result as OkObjectResult;

            Assert.IsType<UsuarioView>(okResponse.Value);
            Assert.Equal(StatusCodes.Status200OK, okResponse.StatusCode);
        }

        private IUsuarioService CreateUsuarioService()
        {
            var usuarioService = new Mock<IUsuarioService>();
            usuarioService
                .Setup(x => x.Add(It.IsAny<Usuario>(), It.IsAny<EnumUsuarioPerfil>()))
                .Callback((Usuario usuario, EnumUsuarioPerfil perfil) =>
                    _createdAsync = true
                )
                .ReturnsAsync((Usuario usuario, EnumUsuarioPerfil perfil)
                    => new Usuario()
                );

            return usuarioService.Object;
        }
    }
}
