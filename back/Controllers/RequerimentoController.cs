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
    public class RequerimentoController : BaseController
    {
        private readonly ILogger<RequerimentoController> _logger;
        private readonly IRequerimentoService _service;

        public RequerimentoController(ILogger<RequerimentoController> logger, IRequerimentoService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetRequerimentos([FromQuery] Paginacao paginacao, [FromQuery] RequerimentoFiltro filtro, [FromQuery] Ordenacao ordenacao)
        {
            try
            {
                var result = _service.Get(paginacao, filtro, ordenacao);
                return Ok(new Paginacao<RequerimentoView>
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
        public IActionResult GetRequerimento([FromRoute] long id, [FromQuery] IEnumerable<string> includes)
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
        public IActionResult PostRequerimento([FromBody] RequerimentoView requerimento)
        {
            try
            {
                var result = _service.Add(requerimento.ToModel());
                return CreatedAtAction(nameof(GetRequerimento), new { id = result.Id }, result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpPut]
        public IActionResult PutRequerimento([FromBody] RequerimentoView requerimento)
        {
            try
            {
                var result = _service.Update(requerimento.ToModel());
                return Ok(result.ToView());
            }
            catch (Exception e)
            {
                return BadRequest(new Erro { Mensagem = e.Message, Detalhes = e.InnerException?.Message }); ;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRequerimento([FromRoute] long id)
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
