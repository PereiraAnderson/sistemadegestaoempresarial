using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;
using SGE.Services.Interfaces;

namespace SGE.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly IPontoService _pontoService;

        public UsuarioService(IUsuarioRepository repo, IPontoService pontoService,
            IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _repo = repo;
            _serviceProvider = serviceProvider;
            _pontoService = pontoService;
            _configuration = configuration;
        }

        public IEnumerable<Usuario> Get(UsuarioFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(filtro, ordenacao).AsEnumerable();

        public Paginacao<Usuario> Get(Paginacao paginacao, UsuarioFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(paginacao, filtro, ordenacao);

        public Usuario Get(long key, IEnumerable<string> includes = null) =>
            _repo.Get(key, includes);

        public Usuario GetByCPF(string cpf, IEnumerable<string> includes = null) =>
            _repo.GetByCPF(cpf, includes);

        public Usuario GetByLogin(string email, IEnumerable<string> includes = null) =>
            _repo.GetByLogin(email, includes);

        public Usuario Add(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            var ret = _repo.Add(usuario);
            _repo.SaveChanges();
            return ret;
        }

        public Usuario Update(Usuario usuario)
        {
            var ret = _repo.Update(usuario);
            _repo.SaveChanges();
            return ret;
        }

        public Usuario Remove(Usuario usuario)
        {
            var ret = _repo.Remove(usuario);
            _repo.SaveChanges();
            return ret;
        }

        public Usuario Remove(long id)
        {
            var ret = _repo.Remove(id);
            _repo.SaveChanges();
            return ret;
        }

        public LoginOut Login(LoginIn login)
        {
            var usuario = _repo.GetByLogin(login.Login);
            if (usuario == null)
                throw new Exception("Login e/ou senha incorreta");

            var correta = BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha);
            if (!correta)
                throw new Exception("Login e/ou senha incorreta");

            return new LoginOut
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Perfil = usuario.Perfil
            };
        }

        public Usuario AlterarSenha(AlterarSenha alterarSenha)
        {
            var usuario = _repo.GetByLogin(alterarSenha.Login);
            if (usuario == null)
                throw new Exception("Login e/ou senha incorreta");

            var correta = BCrypt.Net.BCrypt.Verify(usuario.Senha, alterarSenha.SenhaAtual);
            if (!correta)
                throw new Exception("Login e/ou senha incorreta");

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(alterarSenha.SenhaNova);

            return usuario;
        }
    }
}