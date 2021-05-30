using SGE.Context.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGE.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using SGE.Services;
using SGE.Services.Interfaces;
using SGE.Context.Repositories.Interfaces;
using SGE.Context.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddSwaggerDocument(configure =>
            {
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Swagger SGE";
                    document.Info.Description = "Swagger com todas as rotas acessíveis da SGE";
                    document.Info.TermsOfService = "None";
                };

                configure.GenerateEnumMappingDescription = true;
            });

            services.AddAuthorization();
            services.AddControllers()
                .AddJsonOptions(option => option.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddMvc();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
               {
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = false,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Autentication:SecurityKey"])),
                       ValidIssuer = _configuration["Autentication:Issue"],
                       ValidAudience = _configuration["Autentication:Audience"],
                       ClockSkew = TimeSpan.Zero
                   };
               }).AddApplicationCookie();

            services.AddDbContext<SGEDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("SqlServerSGE")));

            services.AddIdentityCore<Usuario>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                    })
                .AddEntityFrameworkStores<SGEDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddTransient<IPontoRepository, EnderecoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient<IPontoService, EnderecoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UsePathBase("/api");

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseResponseCompression();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

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
