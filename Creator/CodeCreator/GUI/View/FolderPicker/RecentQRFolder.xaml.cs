using GUI.View.FolderPicker;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for RecentQRFolder.xaml
    /// </summary>
    public partial class RecentQRFolder : UserControl
    {
        protected readonly RecentQRFolderViewModel vm;


        public RecentQRFolder()
        {
            InitializeComponent();

            try
            {

                DataContext = vm = new RecentQRFolderViewModel(Global.OutputPath, new QRFileItemComparer());
            }
            catch(Exception ex)
            {
                
            }
        }


        private void _container_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(vm.SelectedItem.Path);
        }
    }


    
}
