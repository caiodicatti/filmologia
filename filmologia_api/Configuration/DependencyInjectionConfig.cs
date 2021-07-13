using filmologia_api.Application;
using filmologia_api.Application.Interface;
using filmologia_api.Repository;
using filmologia_api.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace filmologia_api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //services.AddScoped<DatabaseContext, DatabaseContext>();
            //services.AddSingleton<IConfiguration>(Configuration);

            //services.AddDistributedMemoryCache();
            //services.AddSession();

            //services.AddTransient<UsuarioRepository, UsuarioRepository>();
            services.AddTransient<IAppUser, AppUser>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}
