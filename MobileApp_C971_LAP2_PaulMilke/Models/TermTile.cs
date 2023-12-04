using CommunityToolkit.Mvvm.Messaging;
using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.Controls
{
    public class TermTile : Frame
    {
        //The following are the two bindable properties for each tile. 
        //One sets receives and sets the Term object for the tile. The other is a command property to register when it's tapped. 
        public static readonly BindableProperty TermDataProperty = BindableProperty.Create(nameof(TermData), typeof(Term), typeof(TermTile), default(Term));
        public static readonly BindableProperty TileCommandProperty = BindableProperty.Create(nameof(TileCommand), typeof(ICommand), typeof(TermTile), default(ICommand));

        SchoolDatabase schoolDatabase; 

        public Term TermData
        {
            get { return (Term)GetValue(TermDataProperty);  }
            set { SetValue(TermDataProperty, value); }
        }

        public ICommand TileCommand
        {
            get {  return(ICommand)GetValue(TileCommandProperty);}
            set { SetValue(TileCommandProperty, value);}
        }

        //The main TermTile constructor. This doesn't take any parameters due to the bindable properties above. 
        //This calls the create grid method and sets some simple visual settings. 
        public TermTile()
        {
            schoolDatabase = new SchoolDatabase();
            this.BorderColor = Color.FromRgb(0, 0, 0);
            this.Padding = 10; 
            this.Margin = 5;
            this.CreateGrid(); 
        }

        //The create grid method creates the actual "grid" for each tile layout. This includes 
        //setting the title, dates, and options menu button. 
        public void CreateGrid()
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Auto)},
                    new ColumnDefinition {Width = new GridLength(2, GridUnitType.Auto) },
                    new ColumnDefinition {Width = new GridLength(3, GridUnitType.Star) },
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            var titleLabel = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 18,
                Margin = 5
            };
            titleLabel.SetBinding(Label.TextProperty, new Binding("TermData.Title", source: this));

            var sLabel = new Label
            {
                Text = "Start",
                Margin = new Thickness(left:  5, top: 5, right: 5, bottom: 0),
                TextDecorations = TextDecorations.Underline
            };

            var eLabel = new Label
            {
                Text = "End",
                Margin = 5,
                TextDecorations = TextDecorations.Underline
            };
 
            var startLabel = new Label
            {
                Margin = 5,
            };
            startLabel.SetBinding(Label.TextProperty, new Binding("TermData.Start.Date", source: this, stringFormat: "{0:MM/dd/yyyy}"));

            var endLabel = new Label
            {
                Margin = 5, 
            };
            endLabel.SetBinding(Label.TextProperty, new Binding("TermData.End.Date", source: this, stringFormat: "{0:MM/dd/yyyy}"));

            var menuButton = new ImageButton
            {
                Source = "ellipses.png",
                HorizontalOptions = LayoutOptions.End,
                HeightRequest = 24,
                WidthRequest = 24,
                Command = new Command(ShowMenu)
            };

            //Set position of grid elements. 
            Grid.SetRow(titleLabel, 0);
            Grid.SetColumn(titleLabel, 0);
            grid.Children.Add(titleLabel);

            Grid.SetRow(sLabel, 1);
            Grid.SetColumn(sLabel, 0);
            grid.Children.Add(sLabel);

            Grid.SetRow(eLabel, 1);
            Grid.SetColumn(eLabel, 2);
            grid.Children.Add(eLabel); 

            Grid.SetRow(startLabel, 2);
            Grid.SetColumn(startLabel, 0);
            grid.Children.Add(startLabel);

            Grid.SetRow(endLabel, 2);
            Grid.SetColumn(endLabel, 2);
            grid.Children.Add(endLabel);

            Grid.SetRow(menuButton, 0);
            Grid.SetColumn(menuButton, 3);
            grid.Children.Add(menuButton);

            Content = grid;

            var tapGesterRecognizer = new TapGestureRecognizer();
            tapGesterRecognizer.Tapped += OnTileTapped;
            this.GestureRecognizers.Add(tapGesterRecognizer);
        }

        //Tap event method that calls the TileCommand via binding. This command calls the navigation method in the MainPageViewModel. 
        public void OnTileTapped(object sender, EventArgs e)
        {
            TileCommand?.Execute(TermData.Id); 
        }

        //Show menu is called when the (...) button is tapped. This pops a menu with the following options. 
        private async void ShowMenu()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet
                (
                $"{TermData.Title} - Options",
                "Close",
                null,
                "Edit",
                "Details",
                "Delete"
                );
            switch (action)
            {
                case "Edit":
                    WeakReferenceMessenger.Default.Send(new EditTermMessage { UpdatedTerm = TermData});
                    break;
                case "Details":
                    TileCommand?.Execute(TermData.Id); 
                    break;
                case "Delete":
                    await schoolDatabase.DeleteTermAsync(TermData);
                    break; 
            }
        }
    }
}
  