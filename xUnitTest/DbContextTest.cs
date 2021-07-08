using System;
using Microsoft.EntityFrameworkCore;
using SGE.Context;

public class DbContextTest
{
    public SGEDbContext DbContext { get; }

    public DbContextTest()
    {
        var options = new DbContextOptionsBuilder<SGEDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        DbContext ??= new SGEDbContext(options);
    }
}