using Core.Models;
using GUI.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ExportItemsPage.xaml
    /// </summary>
    public partial class ExportItemsPage : Window
    {
        public ExportItemsPage(ICollection<YardItem> imgs, Window parent)
        {
            InitializeComponent();


            Owner = parent;

            DataContext = new ExportItemsViewModel(imgs);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            var vm = (ExportItemsViewModel)DataContext;

            AppConfig.Config.ValidUntil = vm.SelectedDate;
            Configuration.ConfigLoader.Save(AppConfig.Config);
        }
    }


    

}
