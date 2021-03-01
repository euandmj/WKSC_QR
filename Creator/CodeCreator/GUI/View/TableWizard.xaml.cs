using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for TableWizard.xaml
    /// </summary>
    public partial class TableWizard : UserControl
    {
        public event EventHandler NewConfigSaved;

        private readonly TableWizardViewModel vm;

        public TableWizard()
        {
            InitializeComponent();
            DataContext = vm = new TableWizardViewModel();
        }
        private int GetCol(object letter)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabet.IndexOf((char)letter);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.Config = new Configuration.Config
            {
                SpreadsheetFile = vm.SelectedCSVFile,
                ClassColumn = GetCol(cbClass.SelectedItem),
                OwnerColumn = GetCol(cbOwner.SelectedItem),
                RowColumnn = GetCol(cbRow.SelectedItem),
                SailColumn = GetCol(cbSail.SelectedItem)
            };

            Configuration.ConfigLoader.Save(AppConfig.Config);

            NewConfigSaved?.Invoke(null, EventArgs.Empty);
        }
    }
}
