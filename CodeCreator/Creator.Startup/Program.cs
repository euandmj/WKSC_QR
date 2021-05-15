using Autofac;
using GUI;
using System;
using System.IO;
using System.Windows;
using static System.Console;

namespace Creator.Startup
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (!Directory.Exists(Global.AppPath))
            {
                Directory.CreateDirectory(Global.AppPath);
            }

            if (!Directory.Exists(Global.OutputPath))
            {
                Directory.CreateDirectory(Global.OutputPath);
            }

            try
            {
                CreateDependencies();

                WriteLine("Starting up. You can minimise this window...");

                var app = new App();
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception ex)
            {
                WriteLine("Unexpected error occurred. Please try relaunching the program: ");
                WriteLine(ex.Message);
                WriteLine("press any key to exit");                
                ReadKey();
            }
        }

        static void CreateDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<GUI.Modules.Module>();

            Core.DependencyInjection.ContainerClass.Initialise(builder.Build());
        }
    }
}
