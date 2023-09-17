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
            // read configuration values from local settings json & environment variables
            // NOTE - the Build Action and Copy to Output Directory properties of the JSON file must be set to Content and Copy if newer (or Copy always) respectively
            // Microsoft.Extensions.Configuration.Json
            // Microsoft.Extensions.Configuration.EnvironmentVariables
            var configurationSettings = new ConfigurationBuilder()
                //.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Microsoft.Extensions.Configuration.Binder
            //var repoSettings = configurationSettings.Get<GameRepositorySettings>();

            // Microsoft.Extensions.DependencyInjection
            //services.AddSingleton(repoSettings);
            services.AddSingleton<IGameController, GameController>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IGameState, GameState>();
            services.AddSingleton<IMinesState, MinesState>();

            return services;
        }
    }
}