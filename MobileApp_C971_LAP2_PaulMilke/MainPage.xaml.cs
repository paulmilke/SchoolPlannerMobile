using MobileApp_C971_LAP2_PaulMilke.Controls;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            var stackLayout = new StackLayout { Padding = 15, Spacing = 15 };

            string termTitle = "My First Term!";

            stackLayout.Children.Add(new TermTile(termTitle));

            Content = new ScrollView { Content = stackLayout };
        }

        /*private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }*/
    }
}