using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    class ReportsViewModel : BaseViewModel
    {
        private readonly SchoolDatabase schoolDatabase;
        public ObservableCollection<Class> ClassList { get; set; } = new ObservableCollection<Class>();

        private string reportTitle; 
        public string ReportTitle
        {
            get { return reportTitle; }
            set { reportTitle = value; OnPropertyChanged(); }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible; set { isVisible = value; OnPropertyChanged(); }
        }

        public ReportsViewModel(INavigationService navigationService) : base(navigationService) 
        {
            schoolDatabase = new SchoolDatabase();
            IsVisible = false;
        }

        public ICommand CompletedCoursesCommand => new Command(async (async) => await GetCompletedCourses());
        public ICommand UpcomingCoursesCommand => new Command(async (async) => await GetUpcomingCourses());
        private async Task GetCompletedCourses()
        {
            ClassList.Clear();
            var classList = await schoolDatabase.GetCompletedCoursesAsync("Completed"); 
            if(classList.Count > 0)
            {
                ReportTitle = "Completed Classes";
                IsVisible = true;
                foreach (Class c in classList)
                {
                    ClassList.Add(c);
                }
            }
            else
            {
                IsVisible = false;
                ReportTitle = "Nothing Found";
            }

            
        }

        private async Task GetUpcomingCourses()
        {
            ClassList.Clear();
            var classList = await schoolDatabase.GetCompletedCoursesAsync("Plan to Take");
            if (classList.Count > 0)
            {
                ReportTitle = "Upcoming Classes";
                IsVisible = true;
                foreach (Class c in classList)
                {
                    ClassList.Add(c);
                }
            }
            else
            {
                IsVisible = false;
                ReportTitle= "Nothing Found";
            }
        }
    }
}
