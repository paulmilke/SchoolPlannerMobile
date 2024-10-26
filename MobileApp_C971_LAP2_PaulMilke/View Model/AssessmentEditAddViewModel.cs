using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.Models;
using System.Windows.Input;
using Plugin.LocalNotification;
using MobileApp_C971_LAP2_PaulMilke.Interfaces;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    [QueryProperty(nameof(AssessmentType), "ITEM")]
    [QueryProperty(nameof(IsAdding), "BOOL")]
    [QueryProperty(nameof(ClassID), "CLASSID")]
    [QueryProperty(nameof(AssessmentID), "OBJECTID")]
    public class AssessmentEditAddViewModel : BaseViewModel
    {
        IRestService _restService; 
        SchoolDatabase schoolDatabase;
        private readonly Services.INotificationService notificationService;
        public ICommand AddAssessmentCommand { get; }
        public ICommand EditAssessmentCommand { get; }
        public ICommand DeleteAssessmentCommand { get; }

        private int classID;
        public int ClassID
        {
            get => classID;
            set
            {
                classID = value;
                OnPropertyChanged();
            }
        }

        private int assessmentID;
        public int AssessmentID
        {
            get { return assessmentID; }
            set
            {
                assessmentID = value;
                OnPropertyChanged();
            }
        }

        private string assessmentType;
        public string AssessmentType
        {
            get => assessmentType;
            set
            {
                assessmentType = value;
                OnPropertyChanged();
            }
        }

        private bool isAdding;
        public bool IsAdding
        {
            get => isAdding;
            set
            {
                isAdding = value;
                OnPropertyChanged();
            }
        }

        private Assessment currentAssessment; 
        public Assessment CurrentAssessment
        {
            get { return currentAssessment; } 
            
            set
            {
                currentAssessment = value;
                OnPropertyChanged();
            }
        }


        public AssessmentEditAddViewModel(INavigationService navigationService, Services.INotificationService _notificationService, IRestService restService) : base(navigationService) 
        {
            _restService = restService;
            schoolDatabase = new SchoolDatabase();
            notificationService = _notificationService;
            currentAssessment = new Assessment();
            AddAssessmentCommand = new Command(async () => await AddAssessment());
            EditAssessmentCommand = new Command(SwapIsAdding);
            DeleteAssessmentCommand = new Command(async () => await DeleteAssessment());
        }

        public async Task OnNavigatedToAsync()
        {
            await SetValues();
        }

        private async Task SetValues()
        {
            if(AssessmentID != 0) 
            {
                CurrentAssessment = await _restService.GetSingleAssessmentAsync(AssessmentID); 
                ClassID = CurrentAssessment.ClassId;
                AssessmentName = CurrentAssessment.AssessmentName; 
                AssessmentType = CurrentAssessment.AssessmentType;  
                StartDate = CurrentAssessment.StartDate;
                EndDate = CurrentAssessment.EndDate;
            }
            else
            {
                StartDate = DateTime.Today;
                EndDate = DateTime.Today;
            }

        }

        private void SwapIsAdding()
        {
            IsAdding = true; 
        }

        public async Task DeleteAssessment()
        {
            await _restService.DeleteAssessmentAsync(CurrentAssessment.Id); 
            await NavigateBack(); 
        }

        public async Task AddAssessment()
        {
            
            CurrentAssessment.ClassId = ClassID; 
            CurrentAssessment.AssessmentName = AssessmentName;
            CurrentAssessment.AssessmentType = AssessmentType;
            CurrentAssessment.StartDate = StartDate;
            CurrentAssessment.EndDate = EndDate;
            if(AssessmentName == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Assessment must have a title.", "Okay");
            }
            else if(StartDate.Date == EndDate.Date || StartDate > EndDate)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "The start date must be before the end date.", "Okay");
            }
            else
            {
                await schoolDatabase.SaveAssessmentAsync(CurrentAssessment);
                await ScheduleAssessmentNotificationsAsync();
                await NavigateBack(); 
            }

        }

        public async Task NavigateBack()
        {
            await NavigationService.GoBackAsync(); 
        }

        public async Task ScheduleAssessmentNotificationsAsync()
        {
            //Start date notification
            var sTime = DateTime.Now;
            if (CurrentAssessment.StartDate == DateTime.Today)
            {
                sTime = DateTime.Now.AddSeconds(1);
            }
            else
            {
                sTime = CurrentAssessment.StartDate.AddHours(8); 
            }

            var startNotificationRequest = new NotificationRequest
            {
                NotificationId = int.Parse($"{CurrentAssessment.Id}1"),
                Title = "Assessment Starting",
                Description = $"Your assessment: {CurrentAssessment.AssessmentName} starts today!",
                Schedule =
                {
                    NotifyTime = sTime,
                }
            };

            //End date notification

            var eTime = DateTime.Now;
            if (CurrentAssessment.EndDate == DateTime.Today)
            {
                eTime = DateTime.Now.AddSeconds(1);
            }
            else
            {
                eTime= CurrentAssessment.EndDate.AddHours(8);
            }

            var endNotificationRequest = new NotificationRequest
            {
                NotificationId = int.Parse($"{CurrentAssessment.Id}2"),
                Title = "Assessment Ending",
                Description = $"Your assessment: {CurrentAssessment.AssessmentName} ends today!",
                Schedule =
                {
                    NotifyTime= eTime,
                }
            };

            await notificationService.ScheduleNotificationAsync(startNotificationRequest);
            await notificationService.ScheduleNotificationAsync(endNotificationRequest); 
        }

        private string assessmentName; 
        public string AssessmentName
        {
            get => assessmentName;
            set
            {
                assessmentName = value;
                OnPropertyChanged();
            }
        }


        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime endDate; 
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged();
            }
        }
    }
}
