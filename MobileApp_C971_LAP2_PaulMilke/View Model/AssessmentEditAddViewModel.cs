using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    [QueryProperty(nameof(AssessmentType), "ITEM")]
    [QueryProperty(nameof(IsAdding), "BOOL")]
    [QueryProperty(nameof(ClassID),"CLASSID")]
    public class AssessmentEditAddViewModel : BaseViewModel
    {
        SchoolDatabase schoolDatabase;
        public ICommand AddAssessmentCommand { get; }

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


        public AssessmentEditAddViewModel(INavigationService navigationService) : base(navigationService) 
        {
            schoolDatabase = new SchoolDatabase();
            currentAssessment = new Assessment();
            AddAssessmentCommand = new Command(async () => await AddAssessment());
            SetValues();
        }

        public void InitializeAsync()
        {

        }

        private void SetValues()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public async Task AddAssessment()
        {
            CurrentAssessment.ClassId = ClassID; 
            CurrentAssessment.AssessmentName = AssessmentName;
            CurrentAssessment.AssessmentType = AssessmentType;
            CurrentAssessment.StartDate = StartDate;
            CurrentAssessment.EndDate = EndDate;
            await schoolDatabase.SaveAssessmentAsync(CurrentAssessment);
        }

        private string assessmentName; 
        public string AssessmentName
        {
            get => assessmentName;
            set
            {
                assessmentName = value;
                OnPropertyChanged(nameof(assessmentName));
            }
        }


        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(startDate));
            }
        }

        private DateTime endDate; 
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(endDate));
            }
        }
    }
}
