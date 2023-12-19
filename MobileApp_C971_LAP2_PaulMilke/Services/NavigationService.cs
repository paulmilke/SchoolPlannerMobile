using MobileApp_C971_LAP2_PaulMilke.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    public class NavigationService : INavigationService
    {

        public async Task NavigateToAsync(string routeName)
        {
            await Shell.Current.GoToAsync(routeName); 
        }

        public async Task NavigateToAsync(string routeName, int objectID)
        {
            var routeWithParameter = $"{routeName}?OBJECTID={objectID}";
            await Shell.Current.GoToAsync(routeWithParameter); 
        }

        public async Task NavigateToAsync(string routeName, string item, bool isAdding, int classID)
        {
            var routeWithParameter = $"{routeName}?ITEM={item}&BOOL={isAdding}&CLASSID={classID}";
            await Shell.Current.GoToAsync(routeWithParameter);
        }


        public async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }

    }
}
