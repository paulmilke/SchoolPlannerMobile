using CommunityToolkit.Maui.Views;
using MobileApp_C971_LAP2_PaulMilke.Controls;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;
using MobileApp_C971_LAP2_PaulMilke.Views;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            //Initialized mainpage and binds with MainPageViewModel while creating new navigationService. 
            InitializeComponent();
            BindingContext = new MainPageViewModel(new NavigationService());
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            this.ShowPopup(new AddNewTermPopup(new AddNewTermPopupViewModel(new NavigationService())));
        }

    }
}