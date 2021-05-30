using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SGE.Controllers.Utils;
using SGE.Context.Models;
using SGE.Extensions;
using SGE.Infra.Enums;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;
using SGE.Infra.Views.Models;
using SGE.Services.Interfaces;
using System.Threading.Tasks;

namespace SGE.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _service;
        private readonly IServiceProvider _serviceProvider;
        private SignInManager<Usuario> _signInManager;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService service, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _service = service;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        [Authorize(Roles = "SGE, EMPRESA, FUNCIONARIO")]
        public IActionResult GetUsuarios([FromQuery] Paginacao paginacao, [FromQuery] UsuarioFiltro filtro, [FromQuery] Ordenacao ordenacao)
        {
            try
            {
                var result = _service.Get(paginacao, filtro, ordenacao);
                return Ok(new Paginacao<UsuarioView>
                {
                    TotalItens = result.TotalItens,
                    NumeroPagina = result.NumeroPagina,
                    TamanhoPagina = result.TamanhoPagina,
                    TotalPaginas = result.TotalPaginas,
                    ListaItens = result.ListaItens.Select(x => x.ToView())
                });
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SGE, EMPRESA, FUNCIONARIO")]
        public IActionResult GetUsuario([FromRoute] string id, [FromQuery] IEnumerable<string> includes)
        {
            try
            {
                var result = _service.Get(id, includes);

                return Ok(result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuarios([FromBody] UsuarioView usuario)
        {
            switch (UserPerfil)
            {
                case EnumUsuarioPerfil.SGE:
                    break;
                default:
                    if (usuario.Perfil != EnumUsuarioPerfil.FUNCIONARIO)
                        return BadRequest(new Erro { Mensagem = "Não é possível criar este perfil de usuário." });
                    break;
            }

            try
            {
                var result = await _service.Add(usuario.ToModel(), usuario.Perfil);
                return CreatedAtAction(nameof(GetUsuario), new { id = result.Id }, result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message });
            }
        }

        [HttpPut]
        [Authorize(Roles = "SGE, EMPRESA, FUNCIONARIO")]
        public IActionResult PutUsuario([FromBody] UsuarioView usuario)
        {
            try
            {
                var result = _service.Update(usuario);
                return Ok(result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpPut("AlteraSenha")]
        [Authorize(Roles = "SGE, EMPRESA, FUNCIONARIO")]
        public IActionResult PutUsuarioAlteraSenha([FromBody] AlterarSenha alterarSenha)
        {
            try
            {
                var result = _service.AlterarSenha(alterarSenha);
                return Ok(result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SGE, EMPRESA, FUNCIONARIO")]
        public IActionResult DeleteUsuarios([FromRoute] string id)
        {
            try
            {
                var result = _service.Remove(id);
                return Ok(result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginIn login)
        {
            try
            {
                var result = _service.Login(login).Result;
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpPost("Logout")]
        [Authorize(Roles = "SGE, EMPRESA, FUNCIONARIO")]
        public IActionResult Logout()
        {
            _signInManager ??= _serviceProvider.GetRequiredService<SignInManager<Usuario>>();
            _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
