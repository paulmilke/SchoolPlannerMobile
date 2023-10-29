using MobileApp_C971_LAP2_PaulMilke.Controls;
using MobileApp_C971_LAP2_PaulMilke.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MobileApp_C971_LAP2_PaulMilke.Views;
using MobileApp_C971_LAP2_PaulMilke.Models;
using System.Collections.Generic;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{

    public class MainPageViewModel : BaseViewModel
    {
        private readonly SchoolDatabase _schoolDatabase;
        public Term test = new Term("Paul", DateTime.Now, DateTime.Now);
        public ObservableCollection<TermTile> TermList { get; set; } = new ObservableCollection<TermTile>();

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _schoolDatabase = new SchoolDatabase();
            InitAsync().ConfigureAwait(false);
        }
    

    private async Task InitAsync()
        {
            await _schoolDatabase.SaveTermAsync(test);
            var terms = await _schoolDatabase.GetTermsAsync();

            foreach (Term term in terms)
            {
                TermTile tile = new TermTile { TermData = term };
                TermList.Add(tile);
            }
        }

        public ICommand NavigateToCoursesCommand => new Command<string>(async async => await NavigateToCourses());

        public async Task NavigateToCourses()
        {
            var navigationService = new NavigationService(); 
            await navigationService.NavigateToAsync(nameof(CoursesPage));
        }

    }
}
