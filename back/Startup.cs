using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGE.Context;
using Microsoft.Extensions.Logging;
using System;
using SGE.Services;
using SGE.Services.Interfaces;
using SGE.Context.Repositories.Interfaces;
using SGE.Context.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SGE
{
    public class StartupMigration
    {
        public IConfiguration _configuration { get; }

        public StartupMigration(IConfiguration configuration, IWebHostEnvironment env) =>
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) =>
            services.AddDbContext<SGEDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("SqlServerSGE")));

        public void Configure(IApplicationBuilder app)
        {
        }
    }

    public class Startup
    {
        readonly IConfiguration _configuration;
        readonly IWebHostEnvironment _environment;

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddControllers()
                .AddJsonOptions(option => option.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<SGEDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("SqlServerSGE")));

            services.AddTransient<IPontoRepository, EnderecoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient<IPontoService, PontoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UsePathBase("/api");

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseResponseCompression();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = serviceProvider.GetRequiredService<SGEDbContext>();
            context.Database.Migrate();

            var logger = serviceProvider.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation($"New SGE {_environment.EnvironmentName} Instance.");
        }
    }
}
