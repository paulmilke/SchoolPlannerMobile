using MobileApp_C971_LAP2_PaulMilke.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Controls
{
    internal class TermTile : Frame
    {
        public event EventHandler TileClicked; 

        public TermTile(string title)
        {

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
                Text = title,
                FontAttributes = FontAttributes.Bold,
                FontSize = 18
            };
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
            TileClicked?.Invoke(this, EventArgs.Empty);
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
  