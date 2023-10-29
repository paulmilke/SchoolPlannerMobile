using CommunityToolkit.Mvvm.ComponentModel;
using MobileApp_C971_LAP2_PaulMilke.Services;



namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    public partial class BaseViewModel : ObservableObject
    {
        protected readonly INavigationService NavigationService; 
        public BaseViewModel(INavigationService navigationService) 
        {
            NavigationService = navigationService;
        }

    }
}
