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
        private IConfiguration _configuration;
        private SignInManager<Usuario> _signInManager;
        private UserManager<Usuario> _userManager;
        private IPontoService _enderecoService;

        public UsuarioService(IUsuarioRepository repo, IServiceProvider serviceProvider)
        {
            _repo = repo;
            _serviceProvider = serviceProvider;
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

        public Usuario Add(Usuario usuario, EnumUsuarioPerfil perfil)
        {
            _enderecoService ??= _serviceProvider.GetRequiredService<IPontoService>();
            _userManager ??= _serviceProvider.GetRequiredService<UserManager<Usuario>>();

            usuario.Id = Guid.NewGuid().ToString();
            usuario.DataCriacao = DateTimeOffset.Now;
            usuario.Ativo = true;

            var ret = _userManager.CreateAsync(usuario, usuario.PasswordHash).Result;
            if (!ret.Succeeded)
                throw new Exception(string.Concat(ret.Errors.Select(x => x.Description)));

            var claim = new Claim(ClaimTypes.Role, perfil.ToString());
            _userManager.AddClaimAsync(usuario, claim);

            return usuario;
        }

        public Usuario Update(UsuarioView usuarioView)
        {
            _enderecoService ??= _serviceProvider.GetRequiredService<IPontoService>();
            _userManager ??= _serviceProvider.GetRequiredService<UserManager<Usuario>>();

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

            _userManager ??= _serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var loggedinUser = await _userManager.CheckPasswordAsync(usuario, login.Senha);
            if (!loggedinUser)
                throw new Exception("E-mail e/ou senha incorreta");

            _signInManager ??= _serviceProvider.GetRequiredService<SignInManager<Usuario>>();
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
            _userManager ??= _serviceProvider.GetRequiredService<UserManager<Usuario>>();

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
            _configuration ??= _serviceProvider.GetRequiredService<IConfiguration>();

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