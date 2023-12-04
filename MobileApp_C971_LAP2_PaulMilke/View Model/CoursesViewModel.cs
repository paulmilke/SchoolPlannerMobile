using CommunityToolkit.Mvvm.ComponentModel;
using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    [QueryProperty(nameof(TermId), "OBJECTID")]
    public class CoursesViewModel : BaseViewModel
    {
        SchoolDatabase schoolDatabase;
        private int _termId;
        public int TermId
        {
            get => _termId;
            set
            {
                _termId = value;
                OnPropertyChanged();
            }
        }

        private bool isEnabled; 
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand NavigateToEditCourseCommand => new Command<int>(async (classID) => await NavigateToEditCourse(classID));
        public ICommand CreateNewClassCommand => new Command(async () => await CreateNewClass());

        public ObservableCollection<ClassTile> ClassList { get; set; } = new ObservableCollection<ClassTile>();

        public CoursesViewModel(INavigationService navigationService) : base(navigationService)
        {
            schoolDatabase = new SchoolDatabase();
        }

        public async Task InitializeAsync()
        {
            await RefreshTiles();
        }

        public async Task RefreshTiles()
        {
            ClassList.Clear();
            var list = await schoolDatabase.GetClassesAsync(TermId);
            foreach ( var item in list)
            {
                ClassTile newTile = new ClassTile { ClassData = item };
                ClassList.Add(newTile);
            }

            if (ClassList.Count == 6)
            {
                IsEnabled = false;
                IsVisible = true; 
            }
            else
            {
                IsEnabled = true;
                IsVisible = false;
            }
        }

        public async Task CreateNewClass()
        {
            Class newClass = new Class(TermId, "New Class");
            await schoolDatabase.SaveClassAsync(newClass);
            await RefreshTiles();
        }

        public async Task NavigateToEditCourse(int classID)
        {
            var navigationService = new NavigationService();
            await navigationService.NavigateToAsync(nameof(EditCoursePage), classID);
        }
    }
}
  