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
        public ExportItemsPage(ICollection<YardItem> imgs)
        {
            InitializeComponent();


            DataContext = new ExportItemsViewModel(imgs);

        }
    }


    

}
