using Core.Interfaces;
using Core.Models;
using Data.Providers;
using Data.Services;
using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RecentlyChangedItems.xaml
    /// </summary>
    public partial class RecentlyChangedItems : UserControl, IRefreshable
    {
        private readonly RecentlyChangedItems_ViewModel vm;

        public RecentlyChangedItems()
        {
            InitializeComponent();

            DataContext = vm = new RecentlyChangedItems_ViewModel();
        }

        public void Refresh()
        {
        }
    }

    class RecentlyChangedItems_ViewModel
        : BaseViewModel
    {
        readonly IProvider<YardItem, string> dataStore;

        public RecentlyChangedItems_ViewModel()
        {
            dataStore = new Providers.RecentlyEditedYardItemProvider(5, (items) => OnNewData(items));

            DataSource = new ObservableCollection<YardItem>(dataStore.GetItems());
        }
        
        public ObservableCollection<YardItem> DataSource { get; set; }

        private void OnNewData(IEnumerable<YardItem> items)
        {
            //foreach(var item in items)
            //{
            //    if (DataSource.Contains(item))
            //    {
            //        DataSource
            //    }
            //}
            // TODO: update only those that have changed. 
            DataSource = new ObservableCollection<YardItem>(items);
            OnPropertyChanged(nameof(DataSource));
        }
    }
}
