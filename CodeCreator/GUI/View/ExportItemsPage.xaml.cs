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
        private readonly ExportItemsViewModel vm;

        public ExportItemsPage(ICollection<YardItem> imgs, Window parent)
        {
            InitializeComponent();


            Owner = parent;


            DataContext = vm = new ExportItemsViewModel(imgs)
            {
                Cells = dgcc.CellCollection,
                GetWhiteList = dgcc.GetWhiteList
            };
        }



        private void Window_Closed(object sender, System.EventArgs e)
        {
            var vm = (ExportItemsViewModel)DataContext;

            AppConfig.Config.ValidUntil = vm.SelectedDate;
            Configuration.ConfigLoader.Save(AppConfig.Config);

            dgcc.Dispose();
        }
    }


    

}
