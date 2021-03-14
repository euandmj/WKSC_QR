using GUI.Commands;
using GUI.View;
using System.Windows;
using System.Windows.Input;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            DataContext = this;
        }


        public ICommand OpenHelpCommand
        {
            get => new Command((x) =>
            {

                var help = new HelpPage();
                help.ShowDialog();
            });
        }
    }
}
