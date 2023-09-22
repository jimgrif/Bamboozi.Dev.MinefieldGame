using Bamboozi.Dev.MinefieldGame.Controller;
using Bamboozi.Dev.MinefieldGame.Service;
using Bamboozi.Dev.MinefieldGame.StateRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bamboozi.Dev.MinefieldGame.Bootstrap
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddGameService(this IServiceCollection services)
        {
            services.AddScoped<IGameController, GameController>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameState, GameState>();
            services.AddScoped<IMinesState, MinesState>();

            return services;
        }
    }
}