using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Window
    {
        const string HELP_Import            = @"\Help\Help_Import.docx";
        const string HELP_Export            = @"\Help\Help_Export.docx";
        const string HELP_Duration          = @"\Help\Help_Duration.docx";

        public HelpPage()
        {
            InitializeComponent();
        }

        private void OpenHelp(string which)
        {
            string dir = string.Empty;
            try
            {
                dir = ApplicationDeployment.CurrentDeployment.DataDirectory + which;
                Process.Start(dir);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error opening help file " + dir, ex.Message);
            }
        }

        private void HowToImport_Click(object sender, RoutedEventArgs e)
        {
            OpenHelp(HELP_Import);
        }

        private void HowToDuration(object sender, RoutedEventArgs e)
        {
            OpenHelp(HELP_Duration);
        }

        private void HowToExport_Click(object sender, RoutedEventArgs e)
        {
            OpenHelp(HELP_Export);
        }
    }
}
