using GUI.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentScansPage : ContentPage
    {
        RecentItemsViewModel _viewModel;

        public RecentScansPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RecentItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
