using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string routeName);
        Task GoBackAsync(); 
    }
}
