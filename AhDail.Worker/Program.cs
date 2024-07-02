using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AhDail.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}