using CommunityToolkit.Mvvm.ComponentModel;
using MobileApp_C971_LAP2_PaulMilke.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
