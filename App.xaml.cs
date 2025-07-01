using System.Configuration;
using System.Data;
using System.Windows;
using HydroOffice.Database;
using HydroOffice.Database.Repositories.BaseImplementation;
using HydroOffice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HydroOffice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            DotNetEnv.Env.Load();
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServicesContainer(services);
                })
                .Build();
        }
        private static void ConfigureServicesContainer(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddScoped<MainViewModel>();
            services.AddSingleton(DatabaseSessionHelper.SessionFactory);
            services.AddScoped(factory => DatabaseSessionHelper.SessionFactory.OpenSession());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            _host.Start();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
