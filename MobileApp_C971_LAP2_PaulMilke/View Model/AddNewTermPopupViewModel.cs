using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.Views;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    public class AddNewTermPopupViewModel : ObservableObject
    {
        private SchoolDatabase _schoolDatabase;
        public ICommand AddTermCommand { get; set; }
        private Term currentTerm;
        public AddNewTermPopupViewModel()
        {
            Initialize();
        }

        public AddNewTermPopupViewModel(Term term) : this()
        {
            currentTerm = term;
            TitleText = term.Title;
            StartDate = term.Start;
            EndDate = term.End;
        }

        private void Initialize()
        {
            _schoolDatabase = new SchoolDatabase();
            AddTermCommand = new Command<string>(async async => await AddTermtoTable());
            MinDate = DateTime.Today.AddMonths(-1); 
            MaxDate = DateTime.Today.AddYears(1);
        }


        private string titleText;
        public string TitleText
        {
            get { return titleText; }
            set
            {
                titleText = value;
                OnPropertyChanged();
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
            }
        }

        private DateTime minDate;
        public DateTime MinDate
        {
            get => minDate;
            set
            {
                minDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime maxDate;
        public DateTime MaxDate
        {
            get => maxDate;
            set
            {
                maxDate = value;
                OnPropertyChanged();
            }
        }

        public async Task AddTermtoTable()
        {
            if (TitleText == null || StartDate > EndDate)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid Term Parameters", "Okay");
                return;
            }
            else
            {
                if (currentTerm != null)
                {
                    currentTerm.Title = TitleText;
                    currentTerm.Start = StartDate;
                    currentTerm.End = EndDate;

                    await _schoolDatabase.SaveTermAsync(currentTerm);


                }
                else
                {
                    Term newTerm = new Term(titleText, startDate, endDate);
                    //await _schoolDatabase.SaveTermAsync(newTerm);
                    RestService restApi = new RestService();
                    await restApi.SaveNewTermAsync(newTerm);
                    WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
                }

            }

        }

    }
}
