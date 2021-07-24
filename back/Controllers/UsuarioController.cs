using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGE.Controllers.Utils;
using SGE.Extensions;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;
using SGE.Infra.Views.Models;
using SGE.Services.Interfaces;

namespace SGE.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _service;
        private readonly IServiceProvider _serviceProvider;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService service, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _service = service;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
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
        public IActionResult GetUsuario([FromRoute] long id, [FromQuery] IEnumerable<string> includes)
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
        public IActionResult PostUsuario([FromBody] UsuarioView usuario)
        {
            try
            {
                var result = _service.Add(usuario.ToModel());
                return CreatedAtAction(nameof(GetUsuario), new { id = result.Id }, result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message });
            }
        }

        [HttpPut]
        public IActionResult PutUsuario([FromBody] UsuarioView usuario)
        {
            try
            {
                var result = _service.Update(usuario.ToModel());
                return Ok(result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpPut("AlteraSenha")]
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
        public IActionResult DeleteUsuarios([FromRoute] long id)
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
                var result = _service.Login(login);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }
    }
}
