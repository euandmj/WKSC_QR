using Autofac;
using System.IO;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {

            var builder = new ContainerBuilder();
            _ = builder.RegisterModule(new Modules.Module());
            Core.DependencyInjection.AutofacResolver.Initialise(builder.Build());



        }


    }
}
