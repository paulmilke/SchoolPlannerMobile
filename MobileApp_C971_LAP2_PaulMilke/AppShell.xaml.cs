using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Views;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Sets the routing path for the CoursesPage and Edit page. 
            Routing.RegisterRoute(nameof(CoursesPage), typeof(CoursesPage));
            Routing.RegisterRoute(nameof(EditCoursePage), typeof(EditCoursePage));
            Routing.RegisterRoute(nameof(AssessmentEditAdd), typeof(AssessmentEditAdd));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        }
    }
}