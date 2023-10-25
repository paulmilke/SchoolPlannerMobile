using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    public class NavigationService : INavigationService
    {

        public async Task NavigateToAsync(string routeName)
        {
            await Shell.Current.GoToAsync(routeName); 
        }

        public async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }

    }
}
