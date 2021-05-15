using Core.Interfaces;
using GUI.Commands;
using GUI.View;
using System.Windows;
using System.Windows.Controls;
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
                new HelpPage().ShowDialog();
            });
        }

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is TabItem item)
            {
                if(item.Content is IRefreshable refr)
                {

                }
            }

        }
    }
}
