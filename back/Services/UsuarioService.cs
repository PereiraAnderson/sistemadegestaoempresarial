using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Infra.Enums;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;
using SGE.Infra.Views.Models;
using SGE.Services.Interfaces;

namespace SGE.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IPontoService _pontoService;

        public UsuarioService(IUsuarioRepository repo, IPontoService pontoService,
            UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
            IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _repo = repo;
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _pontoService = pontoService;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IEnumerable<Usuario> Get(UsuarioFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(filtro, ordenacao).AsEnumerable();

        public Paginacao<Usuario> Get(Paginacao paginacao, UsuarioFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(paginacao, filtro, ordenacao);

        public Usuario Get(string key, IEnumerable<string> includes = null) =>
            _repo.Get(key, includes);

        public Usuario GetByCPF(string cpf, IEnumerable<string> includes = null) =>
            _repo.GetByCPF(cpf, includes);

        public Usuario GetByEmail(string email, IEnumerable<string> includes = null) =>
            _repo.GetByEmail(email, includes);

        public async Task<Usuario> Add(Usuario usuario, EnumUsuarioPerfil perfil)
        {
            usuario.Id = Guid.NewGuid().ToString();
            usuario.DataCriacao = DateTimeOffset.Now;
            usuario.Ativo = true;

            var ret = await _userManager.CreateAsync(usuario, usuario.PasswordHash);
            if (!ret.Succeeded)
                throw new Exception(string.Concat(ret.Errors.Select(x => x.Description)));

            var claim = new Claim(ClaimTypes.Role, perfil.ToString());
            await _userManager.AddClaimAsync(usuario, claim);

            return usuario;
        }

        public Usuario Update(UsuarioView usuarioView)
        {
            var usuario = _userManager.FindByIdAsync(usuarioView.Id).Result;
            if (usuario == null)
                throw new Exception("Id inexistente");

            usuario.DataModificacao = DateTimeOffset.Now;
            usuario.CPF = usuarioView.CPF;
            usuario.Nome = usuarioView.Nome;

            var ret = _userManager.UpdateAsync(usuario).Result;
            if (!ret.Succeeded)
                throw new Exception(string.Concat(ret.Errors.Select(x => x.Description)));

            return usuario;
        }

        public Usuario Remove(Usuario usuario)
        {
            var ret = _repo.Remove(usuario);
            _repo.SaveChanges();
            return ret;
        }

        public Usuario Remove(string id)
        {
            var ret = _repo.Remove(id);
            _repo.SaveChanges();
            return ret;
        }

        public async Task<LoginOut> Login(LoginIn login)
        {
            var usuario = _repo.GetByEmail(login.Email);
            if (usuario == null)
                throw new Exception("E-mail e/ou senha incorreta");

            var loggedinUser = await _userManager.CheckPasswordAsync(usuario, login.Senha);
            if (!loggedinUser)
                throw new Exception("E-mail e/ou senha incorreta");

            var result = await _signInManager.PasswordSignInAsync(usuario, login.Senha, false, false);

            if (!result.Succeeded)
                throw new Exception("E-mail e/ou senha incorreta");

            JwtSecurityToken token = CreateToken(usuario);
            var jwtSecurityToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginOut
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Perfil = (EnumUsuarioPerfil)Enum.Parse(typeof(EnumUsuarioPerfil), token.Claims.First(x => x.Type.Equals(ClaimTypes.Role)).Value),
                Token = jwtSecurityToken
            };
        }

        public Usuario AlterarSenha(AlterarSenha alterarSenha)
        {
            var usuario = _userManager.FindByNameAsync(alterarSenha.Email).Result;
            if (usuario == null)
                throw new Exception("E-mail inexistente");

            var ret = _userManager.ChangePasswordAsync(usuario, alterarSenha.SenhaAtual, alterarSenha.SenhaNova).Result;
            if (!ret.Succeeded)
                throw new Exception(string.Concat(ret.Errors.Select(x => x.Description)));

            usuario.DataModificacao = DateTimeOffset.Now;
            ret = _userManager.UpdateAsync(usuario).Result;
            if (!ret.Succeeded)
                throw new Exception(string.Concat(ret.Errors.Select(x => x.Description)));

            return usuario;
        }

        private JwtSecurityToken CreateToken(Usuario usuario)
        {
            var claims = _signInManager.CreateUserPrincipalAsync(usuario).Result;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Autentication:SecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Autentication:Issue"],
                audience: _configuration["Autentication:Audience"],
                claims: claims.Claims,
                signingCredentials: credentials
            );
            return token;
        }
    }
}