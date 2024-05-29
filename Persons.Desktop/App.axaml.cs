using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;

using Persons.Desktop.ViewModels;
using Persons.Desktop.Views;

using System;
using IntiLed.Persons.Core.PersonsProviders;

namespace Persons.Desktop
{
    public partial class App : Application
    {
        //TODO to config"
        private readonly string filename = "persons.json";

        //Service Provider
        public IServiceProvider ServiceProvider { get; private set; }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                BuildServices();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void BuildServices()
        {
            var services = new ServiceCollection();
            services.AddFilePersonsProvider(filename);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}