using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MobileApp_C971_LAP2_PaulMilke.Controls;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;
using MobileApp_C971_LAP2_PaulMilke.Views;
using MobileApp_C971_LAP2_PaulMilke.Models;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            //Initialized mainpage and binds with MainPageViewModel while creating new navigationService. 
            InitializeComponent();
            BindingContext = new MainPageViewModel(new NavigationService());

            WeakReferenceMessenger.Default.Register<EditTermMessage>(this, (recipient, message) =>
            {
                this.ShowPopup(new AddNewTermPopup(new AddNewTermPopupViewModel(message.UpdatedTerm)));
            });
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            this.ShowPopup(new AddNewTermPopup(new AddNewTermPopupViewModel()));
        }

    }
}