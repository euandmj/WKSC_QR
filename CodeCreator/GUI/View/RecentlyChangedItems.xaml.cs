using GUI.ViewModel;
using System.Windows.Controls;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for RecentlyChangedItems.xaml
    /// </summary>
    public partial class RecentlyChangedItems : UserControl
    {
        private readonly RecentlyChangedItemsViewModel vm;

        public RecentlyChangedItems()
        {
            InitializeComponent();

            DataContext = vm = new RecentlyChangedItemsViewModel();
        }
    }

    
}
