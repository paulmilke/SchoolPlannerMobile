using CommunityToolkit.Mvvm.Messaging;
using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    public class SearchPageViewModel : BaseViewModel
    {
        SchoolDatabase schoolDatabase;
        public ObservableCollection<TermTile> TermResults { get; set; } = new ObservableCollection<TermTile>();
        public ObservableCollection<ClassTile> ClassResults { get; set; } = new ObservableCollection<ClassTile>(); 
        private string searchItem; 
        public string SearchItem
        {
            get { return searchItem; } 
            set
            {
                searchItem = value;
                OnPropertyChanged(); 
            }
        }

        private bool showTerms;
        public bool ShowTerms
        {
            get => showTerms;
            set
            {
                showTerms = value;
                OnPropertyChanged();
            }
        }

        private bool showClasses;
        public bool ShowClasses
        {
            get => showClasses;
            set
            {
                showClasses = value;
                OnPropertyChanged();
            }
        }


        public SearchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            schoolDatabase = new SchoolDatabase();
            WeakReferenceMessenger.Default.Register<TermUpdateMessage>(this, async (recipient, message) =>
            {
                await SearchTerms();
            });

        }

        public ICommand SearchTermsCommand => new Command(async (async) => await SearchTerms());
        public ICommand SearchCoursesCommand => new Command(async (async) => await SearchCourses());
        public ICommand NavigateToCoursesCommand => new Command<int>(async (termId) => await NavigateToCourses(termId));
        public ICommand NavigateToEditCourseCommand => new Command<int>(async (classID) => await NavigateToEditCourse(classID));


        public async Task SearchTerms()
        {
            TermResults.Clear();
            ClassResults.Clear();
            List<Term> termsList =  await schoolDatabase.SearchTerms(SearchItem);
            if (termsList.Count > 0)
            {
                ShowTerms = true;
                ShowClasses = false; 
                foreach(Term t in termsList)
                {
                    TermTile termTile = new TermTile { TermData = t };
                    TermResults.Add(termTile);
                }
            }

        }

        private async Task SearchCourses()
        {
            ClassResults.Clear();
            TermResults.Clear();
            List<Class> classList = await schoolDatabase.SearchClasses(SearchItem);
            if (classList.Count > 0)
            {
                ShowClasses = true;
                ShowTerms = false;
                foreach (Class c in classList)
                {
                    ClassTile classTile = new ClassTile { ClassData = c };
                    ClassResults.Add(classTile);
                }
            }
        }

        public async Task NavigateToCourses(int termId)
        {
            var navigationService = new NavigationService();
            await navigationService.NavigateToAsync(nameof(CoursesPage), termId);
        }

        public async Task NavigateToEditCourse(int classID)
        {
            var navigationService = new NavigationService();
            await navigationService.NavigateToAsync(nameof(EditCoursePage), classID);
        }
    }
}
