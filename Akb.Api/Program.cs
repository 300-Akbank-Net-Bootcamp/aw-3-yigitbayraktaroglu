using Akb.Api.Controllers;
namespace Akb.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        webBuilder.UseStartup<Startup>()).Build().Run();
    }
}