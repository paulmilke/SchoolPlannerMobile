﻿using MobileApp_C971_LAP2_PaulMilke.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MobileApp_C971_LAP2_PaulMilke.Views;
using MobileApp_C971_LAP2_PaulMilke.Models;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{

    public class MainPageViewModel : BaseViewModel
    {
        private readonly SchoolDatabase _schoolDatabase;
        public ObservableCollection<TermTile> TermList { get; set; } = new ObservableCollection<TermTile>();

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _schoolDatabase = new SchoolDatabase();
            InitAsync().ConfigureAwait(false);

            WeakReferenceMessenger.Default.Register<TermUpdateMessage>(this, async (recipient, message) =>
            {
                await RefreshTiles();
            });
        }


    

        private async Task InitAsync()
        {
            await RefreshTiles(); 

        }

        private async Task RefreshTiles()
        {
            TermList.Clear(); 
            //var terms = await _schoolDatabase.GetTermsAsync();
            RestService restApi = new RestService();
            var terms = await restApi.RefreshTermsAsync(); 

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
