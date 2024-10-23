using MobileApp_C971_LAP2_PaulMilke.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MobileApp_C971_LAP2_PaulMilke.Views;
using MobileApp_C971_LAP2_PaulMilke.Models;
using CommunityToolkit.Mvvm.Messaging;
using MobileApp_C971_LAP2_PaulMilke.Interfaces;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IRestService _restService;
        public ObservableCollection<TermTile> TermList { get; set; } = new ObservableCollection<TermTile>();

        public MainPageViewModel(INavigationService navigationService, IRestService restService) : base(navigationService)
        {
            _restService = restService;
            WeakReferenceMessenger.Default.Register<TermUpdateMessage>(this, async (recipient, message) =>
            {
                await RefreshTiles();
            });
        }

        public async Task OnNavigatedToAsync()
        {
            await RefreshTiles(); 
        }

        private async Task RefreshTiles()
        {
            TermList.Clear(); 
            var terms = await _restService.RefreshTermsAsync(); 

            foreach (Term term in terms)
            {
                TermTile tile = new TermTile { TermData = term };
                TermList.Add(tile);
            } 

        }

        public ICommand NavigateToCoursesCommand => new Command<int>(async (termId) => await NavigateToCourses(termId));
        public ICommand NavigateToSearchCommand => new Command(async (async) => await NavigateToSearch());
        public ICommand NavigateToReportsCommand => new Command(async (async) => await NavigateToReports());


        private async Task NavigateToReports()
        {
            await NavigationService.NavigateToAsync(nameof(ReportsPage));
        }
        private async Task NavigateToSearch()
        {
            await NavigationService.NavigateToAsync(nameof(SearchPage));
        }

        public async Task NavigateToCourses(int termId)
        {
            await NavigationService.NavigateToAsync(nameof(CoursesPage), termId);
        }

        ~MainPageViewModel()
        {
            WeakReferenceMessenger.Default.Unregister<TermUpdateMessage>(this); 
        }
    }

}
