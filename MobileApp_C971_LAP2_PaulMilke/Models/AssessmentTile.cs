using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.Models
{
    public class AssessmentTile : Frame
    {
        public static readonly BindableProperty AssessmentDataProperty = BindableProperty.Create(nameof(AssessmentData), typeof(Assessment), typeof(AssessmentTile), default(Assessment));
        public static readonly BindableProperty AssessmentTileCommandProperty = BindableProperty.Create(nameof(AssessmentTileCommand), typeof(ICommand), typeof(AssessmentTile), default(ICommand));
        public Assessment AssessmentData
        {
            get { return (Assessment)GetValue(AssessmentDataProperty); }
            set { SetValue(AssessmentDataProperty, value); }
        }

        public ICommand AssessmentTileCommand
        {
            get { return (ICommand)GetValue(AssessmentTileCommandProperty); }
            set { SetValue(AssessmentTileCommandProperty, value); }
        }

        public AssessmentTile()
        {
            this.BorderColor = Color.FromRgb(0, 0, 0);
            this.Padding = 10;
            this.Margin = 5;
            this.CreateGrid();
        }

        private void CreateGrid()
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Auto)},
                    new ColumnDefinition {Width = new GridLength(2, GridUnitType.Auto)},
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            var assessmentTitle = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                Margin = 5
            };
            assessmentTitle.SetBinding(Label.TextProperty, new Binding("AssessmentData.AssessmentName", source: this));
            Grid.SetRow(assessmentTitle, 0);
            Grid.SetColumn(assessmentTitle, 0);
            grid.Children.Add(assessmentTitle);


            var assessmentType = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                Margin = 5
            };
            assessmentType.SetBinding(Label.TextProperty, new Binding("AssessmentData.AssessmentType", source: this));
            Grid.SetRow(assessmentType, 1);
            Grid.SetColumn(assessmentType, 0);
            grid.Children.Add(assessmentType);


            var assessmentStartLabel = new Label
            {
                Text = "Start Date",
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline,
                FontSize = 14,
                Margin = 5
            };
            Grid.SetRow(assessmentStartLabel, 2);
            Grid.SetColumn(assessmentStartLabel, 0);
            grid.Children.Add(assessmentStartLabel);

            var assessmentStart = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 14,
                Margin = 5
            };
            assessmentStart.SetBinding(Label.TextProperty, new Binding("AssessmentData.StartDate", source: this, stringFormat: "{0:MM/dd/yyyy}"));
            Grid.SetRow(assessmentStart, 3);
            Grid.SetColumn(assessmentStart, 0);
            grid.Children.Add(assessmentStart);


            var assessmentEndLabel = new Label
            {
                Text = "End Date",
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline, 
                FontSize = 14,
                Margin = 5
            };
            Grid.SetRow(assessmentEndLabel, 2);
            Grid.SetColumn(assessmentEndLabel, 1);
            grid.Children.Add(assessmentEndLabel);

            var assessmentEnd = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 14,
                Margin = 5
            };
            assessmentEnd.SetBinding(Label.TextProperty, new Binding("AssessmentData.EndDate", source: this, stringFormat: "{0:MM/dd/yyyy}"));
            Grid.SetRow(assessmentEnd, 3);
            Grid.SetColumn(assessmentEnd, 1);
            grid.Children.Add(assessmentEnd);

            Content = grid;

            var tapGester = new TapGestureRecognizer();
            tapGester.Tapped += OnTileTapped;
            this.GestureRecognizers.Add(tapGester);
        }

        public void OnTileTapped(object sender, EventArgs e)
        {
            AssessmentTileCommand?.Execute(AssessmentData.Id);
        }
    }
}
