using MobileApp_C971_LAP2_PaulMilke.Controls;
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
    public class MainPageViewModel :BaseViewModel
    {
        public ObservableCollection<TermTile> TermList { get; set; }

        public MainPageViewModel(INavigationService navigationService) : base(navigationService) 
        {

            TermList = new ObservableCollection<TermTile>
            {
                new TermTile {Title = "Term 1"},
                new TermTile {Title = "Term 2"},
                new TermTile {Title = "Term 3"}
            };
        }

        public ICommand NavigateToCoursesCommand => new Command<string>(async async => await NavigateToCourses());

        public async Task NavigateToCourses()
        {
            var navigationService = new NavigationService(); 
            await navigationService.NavigateToAsync(nameof(CoursesPage));
        }

    }
}
