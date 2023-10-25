using MobileApp_C971_LAP2_PaulMilke.Controls;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            //Initialized mainpage and binds with MainPageViewModel while creating new navigationService. 
            InitializeComponent();
            BindingContext = new MainPageViewModel(new NavigationService()); 

            //Creates stacklayout for tiles to list themselves.
            var stackLayout = new StackLayout { Padding = 15, Spacing = 15 };

            //Creates dummy term tile via the TermTile class. 
            string termTitle = "My First Term!";
            var termTile = new TermTile(termTitle);

            //Adds new child to stacklayout which is the first term tile. 
            stackLayout.Children.Add(termTile);
            Content = new ScrollView { Content = stackLayout };

            //Handles click event of TermTile and navigates to the CoursesPage. 
            termTile.TileClicked += async (sender, e) =>
            {
                var navigationService = new NavigationService();
                await navigationService.NavigateToAsync(nameof(CoursesPage));

            };
        }

    }
}