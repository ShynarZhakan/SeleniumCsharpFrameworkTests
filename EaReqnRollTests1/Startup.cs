
using EaFramework.Config;
using EaFramework.Driver;
using EaApplicationTest.Pages;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace EaReqnRollTests1
{
    public class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            
            services
                .AddSingleton(ConfigReader.ReadConfig())
                .AddScoped<IDriverFixture, DriverFixture>()
                .AddScoped<IDriverWait, DriverWait>()
                .AddScoped<IHomePage, HomePage>()
                .AddScoped<IProductPage, ProductPage>();

            return services;
        }
    }
}
