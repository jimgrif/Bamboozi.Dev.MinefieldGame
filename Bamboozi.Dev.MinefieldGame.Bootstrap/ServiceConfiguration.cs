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
            services.AddSingleton<IGameController, GameController>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IGameState, GameState>();
            services.AddSingleton<IMinesState, MinesState>();

            return services;
        }
    }
}