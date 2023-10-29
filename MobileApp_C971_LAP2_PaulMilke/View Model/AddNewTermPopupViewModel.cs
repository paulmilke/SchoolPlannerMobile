using CommunityToolkit.Maui.Views;
using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.View_Model
{
    public class AddNewTermPopupViewModel : BaseViewModel
    {
        private readonly SchoolDatabase _schoolDatabase;
        public ICommand AddTermCommand { get; set; }
        public AddNewTermPopupViewModel(INavigationService navigationService) : base(navigationService) 
        {
            _schoolDatabase = new SchoolDatabase();
            AddTermCommand = new Command<string>(async async => await AddTermtoTable());
        }

        private string titleText;
        public string TitleText 
        {
            get { return titleText; }
            set 
            {  
                titleText = value; 
                OnPropertyChanged(nameof(TitleText));
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

        public async Task AddTermtoTable()
        {
            Term newTerm = new Term(titleText, startDate, endDate);
            await _schoolDatabase.SaveTermAsync(newTerm);
        }

    }
}
