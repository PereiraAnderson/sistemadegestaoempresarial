using System;
using System.Collections.Generic;
using System.Linq;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;
using SGE.Services.Interfaces;

namespace SGE.Services
{
    public class PontoService : IPontoService
    {
        private readonly IPontoRepository _repo;

        public PontoService(IPontoRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Ponto> Get(PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(filtro, ordenacao).AsEnumerable();

        public Paginacao<Ponto> Get(Paginacao paginacao, PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(paginacao, filtro, ordenacao);

        public Ponto Get(long id, IEnumerable<string> includes = null) =>
            _repo.Get(id, includes);

        public RelatorioPonto GeraRelatorio(long usuarioId)
        {
            var pontos = _repo.Get(
                new PontoFiltro
                {
                    UsuarioId = usuarioId
                },
                new Ordenacao
                {
                    OrdenaPor = "Data"
                }
            );

            var normalizados = new List<PontoNormalizado>();
            var pNorm = new PontoNormalizado();
            foreach (var p in pontos)
            {
                pNorm.Tarefas += p.Tarefa + ".";

                if (pNorm.Entrada.CompareTo(System.DateTimeOffset.MinValue) == 0)
                    pNorm.Entrada = p.Data;
                else
                {
                    pNorm.Saida = p.Data;
                    pNorm.Jornada = pNorm.Saida.AddTicks(-pNorm.Entrada.Ticks);

                    normalizados.Add(pNorm);
                    pNorm = new PontoNormalizado();
                }
            }

            var ticks1ano = DateTime.MinValue.AddYears(1).Ticks;
            var relatorio = new RelatorioPonto
            {
                Saldo = new DateTime(ticks1ano),
                Pontos = normalizados
            };

            var ticks4horas = DateTime.MinValue.AddHours(4).Ticks;

            normalizados.ForEach(x =>
            {
                var saldoTicks = x.Jornada.Ticks - ticks4horas;
                x.JornadaString = x.Jornada.ToString("HH:mm");
                relatorio.Saldo = relatorio.Saldo.AddTicks(saldoTicks);
            });

            if (relatorio.Saldo.Ticks < ticks1ano)
                relatorio.SaldoString = "-" + new DateTime(ticks1ano)
                .AddTicks(-relatorio.Saldo.Ticks).ToString("HH:mm");
            else
                relatorio.SaldoString = relatorio.Saldo.AddYears(-1).ToString("HH:mm");

            return relatorio;
        }

        public Ponto Add(Ponto ponto)
        {
            var ret = _repo.Add(ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Add(IEnumerable<Ponto> pontos)
        {
            _repo.Add(pontos);
            _repo.SaveChanges();
        }

        public Ponto Update(Ponto ponto)
        {
            var ret = _repo.Update(ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Update(IEnumerable<Ponto> pontos)
        {
            _repo.Update(pontos);
            _repo.SaveChanges();
        }

        public Ponto Remove(Ponto ponto)
        {
            var ret = _repo.Remove(ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Remove(IEnumerable<Ponto> pontos)
        {
            _repo.Remove(pontos);
            _repo.SaveChanges();
        }

        public Ponto Remove(long id)
        {
            var ret = _repo.Remove(id);
            _repo.SaveChanges();
            return ret;
        }

        public void Remove(IEnumerable<dynamic> ids)
        {
            _repo.Remove(ids);
            _repo.SaveChanges();
        }
    }
}