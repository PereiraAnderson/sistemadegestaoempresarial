using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SGE
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static void Main() =>
            CreateHostBuilder().Build().Run();

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(options =>
                {
                    options.UseStartup<Startup>();
                });
    }
}
