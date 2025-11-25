using EaFramework.Config;
using Microsoft.Extensions.DependencyInjection;
using EaApplicationTest.Pages;


namespace EaApplicationTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(ConfigReader.ReadConfig())
                .AddScoped<IDriverFixture, DriverFixture>()
                .AddScoped<IDriverWait, DriverWait>()
                .AddScoped<IHomePage, HomePage>()
                .AddScoped<IProductPage, ProductPage>();
        }
    }
}
