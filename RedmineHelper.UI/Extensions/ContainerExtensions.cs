using RedmineHelper.Mapper;
using RedmineHelper.Mapper.Abstractions;
using BaseMapper = RedmineHelper.Mapper.Abstractions.BaseMapper;

namespace RedmineHelper.UI.Extensions
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using CommandStorages;
    using Services;
    using Services.Abstractions;
    using Services.Filters;
    using Services.Implementations;
    using States;
    using Views;
    using ViewModels;
    using SimpleInjector;

    public static class ContainerExtensions
    {
        public static void RegisterViews(this Container container)
        {
            container.RegisterSingleton<App>();
            container.RegisterSingleton<MainWindow>();
            container.Register<Login>();
            container.Register<Tasks>();
        }

        public static void RegisterServices(this Container container)
        {
            container.RegisterSingleton<IAccessResolver, RedmineResolver>();
            container.Register<ITaskAdapter, RedmineTaskAdapter>(Lifestyle.Transient);
            container.Register<BaseMapper, RedmineMapper>(Lifestyle.Transient);
            container.Register<PageSwitcher>(Lifestyle.Transient);
            container.Register<TaskFilter>(Lifestyle.Transient);
            container.Register<TasksCommands>(Lifestyle.Transient);
            container.Register<TasksState>(Lifestyle.Transient);
            container.Register<RedmineTaskViewModel>(Lifestyle.Transient);
            container.RegisterHttpFactory();
        }

        private static void RegisterHttpFactory(this Container container)
        {
            IServiceCollection defaultDi = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Configuration", "appsettings.json"), false, true)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Configuration", "apikey.json"), true, true)
                .Build();

            defaultDi.AddHttpClient<RedmineHttpClient>((client) =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("RedmineAddress").Value);
                var key = configuration.GetSection("Key").Value;
                if (!string.IsNullOrEmpty(key))
                    client.DefaultRequestHeaders.Add(configuration.GetSection("AuthorizeHeader").Value, key);
            });

            var defaultServiceProvider = defaultDi.BuildServiceProvider();

            container.Register(() => defaultServiceProvider.GetService<RedmineHttpClient>(), Lifestyle.Transient);

            container.ContainerScope.RegisterForDisposal((IDisposable)defaultServiceProvider);
            container.RegisterInstance(configuration);
        }
    }
}

