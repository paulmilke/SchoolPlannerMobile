using MobileApp_C971_LAP2_PaulMilke.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.Controls
{
    public class TermTile : Frame
    {
        //public event EventHandler TileClicked;
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(TermTile), default(string));
        public static readonly BindableProperty TileCommandProperty = BindableProperty.Create(nameof(TileCommand), typeof(ICommand), typeof(TermTile), default(ICommand));

        public string Title 
        {
            get { return (string)GetValue(TitleProperty); } 
            set { SetValue(TitleProperty, value); }
        }

        public ICommand TileCommand
        {
            get {  return(ICommand)GetValue(TileCommandProperty);}
            set { SetValue(TileCommandProperty, value);}
        }

        public TermTile()
        {
            this.BorderColor = Color.FromRgb(0, 0, 0);
            this.Padding = 10; 
            this.Margin = 5;

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Auto)},
                    new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };
            
            var titleLabel = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 18
            };
            titleLabel.SetBinding(Label.TextProperty, new Binding("Title", source: this));

            Grid.SetRow(titleLabel, 0);
            Grid.SetColumn(titleLabel, 0);
            grid.Children.Add(titleLabel);

            var dateLabel = new Label
            {
                Text = $"{DateTime.Now:d} - {DateTime.Now.AddMonths(3):d}",
            };
            Grid.SetRow(dateLabel, 1);
            Grid.SetColumn(dateLabel, 0); 
            grid.Children.Add(dateLabel);

            var menuButton = new ImageButton
            {
                Source = "ellipses.png", 
                HorizontalOptions = LayoutOptions.End,
                HeightRequest = 24, 
                WidthRequest = 24,
                Command = new Command(ShowMenu)
            };
            Grid.SetRow(menuButton, 0);
            Grid.SetColumn(menuButton, 1);
            grid.Children.Add(menuButton);

            Content = grid;

            var tapGesterRecognizer = new TapGestureRecognizer();
            tapGesterRecognizer.Tapped += OnTileTapped;
            this.GestureRecognizers.Add(tapGesterRecognizer);
        }

        public void OnTileTapped(object sender, EventArgs e)
        {
            TileCommand?.Execute(null); 
        }

        private async void ShowMenu()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet
                (
                "Options",
                "Close",
                null,
                "Edit",
                "Details",
                "Delete"
                );
            switch (action)
            {
                case "Edit":
                    break;
                case "Details":
                    break;
                case "Delete":
                    break; 
            }
        }
    }
}
  