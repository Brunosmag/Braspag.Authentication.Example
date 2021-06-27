using Braspag.Authentication.Application.Services.BraspagTokenOrchestrator;
using Braspag.Authentication.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Braspag.Authentication.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var clientId = Guid.Parse("INSIRA_AQUI_SEU_CLIENTID");
            var clientSecret = "INSIRA_AQUI_SEU_CLIENT_SECRET";

            var serviceProvider = CreateServiceProvider();

            var tokenOrchestrator = serviceProvider.GetService<IBraspagTokenOrchestrator>();
            for (int i = 0; i < 10; i++)
            {
                var result = await tokenOrchestrator.CreateSandboxTokenAsync(clientId, clientSecret);
                Console.WriteLine(result.Token);

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }


        public static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddBraspagAuthentication();
            services.AddMemoryCache();
            services.AddHttpClient();

            return services.BuildServiceProvider();
        }
    }
}
