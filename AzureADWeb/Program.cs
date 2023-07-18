
namespace AzureADWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreareHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreareHostBuilder(string[] build) =>
            Host.CreateDefaultBuilder(build)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}