using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
    public class PontoController : BaseController
    {
        private readonly ILogger<PontoController> _logger;
        private readonly IPontoService _service;

        public PontoController(ILogger<PontoController> logger, IPontoService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetPontos([FromQuery] Paginacao paginacao, [FromQuery] PontoFiltro filtro, [FromQuery] Ordenacao ordenacao)
        {
            try
            {
                var result = _service.Get(paginacao, filtro, ordenacao);
                return Ok(new Paginacao<PontoView>
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
        public IActionResult GetPonto([FromRoute] long id, [FromQuery] IEnumerable<string> includes)
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
        public IActionResult PostPontos([FromBody] PontoView Ponto)
        {
            try
            {
                var result = _service.Add(Ponto.ToModel());
                return CreatedAtAction(nameof(GetPonto), new { id = result.Id }, result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpPut]
        public IActionResult PutPonto([FromBody] PontoView Ponto)
        {
            try
            {
                var result = _service.Update(Ponto.ToModel());
                return Ok(result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePontos([FromRoute] long id)
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
    }
}
