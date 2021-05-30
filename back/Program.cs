using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SGE
{
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
