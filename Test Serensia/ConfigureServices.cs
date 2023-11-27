using Test_Serensia.Interfaces;
using Test_Serensia.Services;

namespace Test_Serensia
{
    public static class ConfigureServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAmTheTestServices, AmTheTestService>();
        }
    }
}
