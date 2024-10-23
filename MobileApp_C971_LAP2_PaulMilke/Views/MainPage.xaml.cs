using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;
using MobileApp_C971_LAP2_PaulMilke.Views;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel ViewModel => (MainPageViewModel)BindingContext;

        public MainPage(MainPageViewModel viewModel)
        {
            //Initialized mainpage and binds with MainPageViewModel. 
            InitializeComponent();
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<EditTermMessage>(this, (recipient, message) =>
            {
                this.ShowPopup(new AddNewTermPopup(new AddNewTermPopupViewModel(message.UpdatedTerm)));
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.OnNavigatedToAsync(); 
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            this.ShowPopup(new AddNewTermPopup(new AddNewTermPopupViewModel()));
        }

    }
}