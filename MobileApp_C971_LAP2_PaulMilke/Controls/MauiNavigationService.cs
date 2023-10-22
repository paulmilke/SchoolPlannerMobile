using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Controls
{
    public class MauiNavigationService : INavigationService
    {
        private readonly Shell _shell;

        public MauiNavigationService(Shell shell)
        {
            _shell = shell;
        }

        public async Task InitializeAsync()
        {

        }

        public async Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {

        }

        public async Task PopAsync()
        {

        }
    }
}
