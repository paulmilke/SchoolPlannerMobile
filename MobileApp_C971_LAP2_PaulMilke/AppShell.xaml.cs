using MobileApp_C971_LAP2_PaulMilke.Controls;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Sets the routing path for the CoursesPage. 
            Routing.RegisterRoute(nameof(CoursesPage), typeof(CoursesPage));
        }
    }
}