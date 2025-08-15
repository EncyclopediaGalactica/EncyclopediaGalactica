using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

internal class Program {
    private static int Main(string[] args) {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

    }
}