using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SGE.Context;
using SGE.Context.Models;
using SGE.Infra.Enums;

public class DbContextTest
{
    public SGEDbContext DbContext { get; }

    public DbContextTest()
    {
        var options = new DbContextOptionsBuilder<SGEDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        DbContext ??= new SGEDbContext(options);
        DbContext.Usuario.AddRange(SeedUsuarios());
        DbContext.Ponto.AddRange(SeedPontos());
        DbContext.SaveChanges();
    }

    public static List<Usuario> SeedUsuarios()
    {
        var usuario1 = new Usuario
        {
            Id = 1,
            DataCriacao = new DateTimeOffset(),
            DataModificacao = null,
            Ativo = true,
            CPF = "11111111111",
            Login = "usuarioUm",
            Nome = "Usuário Um",
            Perfil = EnumUsuarioPerfil.FUNCIONARIO,
            // 123456
            Senha = "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS"
        };

        var usuario2 = new Usuario
        {
            Id = 2,
            DataCriacao = new DateTimeOffset(),
            DataModificacao = null,
            Ativo = true,
            CPF = "22222222222",
            Login = "usuarioDois",
            Nome = "Usuário Dois",
            Perfil = EnumUsuarioPerfil.FUNCIONARIO,
            // 123456
            Senha = "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS"
        };

        return new List<Usuario> { usuario1, usuario2 };
    }

    public static List<Ponto> SeedPontos()
    {
        var hoje = DateTime.Today;
        var ponto1 = new Ponto
        {
            Id = 1,
            DataCriacao = new DateTimeOffset(),
            DataModificacao = null,
            Ativo = true,
            Data = new DateTimeOffset(hoje.AddHours(1)),
            Tarefa = "Ponto 01:00",
            UsuarioId = 1
        };
        var ponto2 = new Ponto
        {
            Id = 2,
            DataCriacao = new DateTimeOffset(),
            DataModificacao = null,
            Ativo = true,
            Data = new DateTimeOffset(hoje.AddHours(2)),
            Tarefa = "Ponto 02:00",
            UsuarioId = 1
        };
        var ponto3 = new Ponto
        {
            Id = 3,
            DataCriacao = new DateTimeOffset(),
            DataModificacao = null,
            Ativo = true,
            Data = new DateTimeOffset(hoje.AddHours(3)),
            Tarefa = "Ponto 03:00",
            UsuarioId = 1
        };
        var ponto4 = new Ponto
        {
            Id = 4,
            DataCriacao = new DateTimeOffset(),
            DataModificacao = null,
            Ativo = true,
            Data = new DateTimeOffset(hoje.AddHours(23)),
            Tarefa = "Ponto 23:00",
            UsuarioId = 2
        };
        var ponto5 = new Ponto
        {
            Id = 5,
            DataCriacao = new DateTimeOffset(),
            DataModificacao = null,
            Ativo = true,
            Data = new DateTimeOffset(hoje.AddDays(1).AddHours(1)),
            Tarefa = "Ponto 01:00",
            UsuarioId = 2
        };

        return new List<Ponto> { ponto1, ponto2, ponto3, ponto4, ponto5 };
    }
}