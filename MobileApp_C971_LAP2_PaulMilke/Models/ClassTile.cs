using MobileApp_C971_LAP2_PaulMilke.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp_C971_LAP2_PaulMilke.Models
{
    public class ClassTile : Frame
    {
        SchoolDatabase schoolDatabase;
        public static readonly BindableProperty ClassDataProperty = BindableProperty.Create(nameof(ClassData), typeof(Class), typeof(ClassTile), default(Class));
        public static readonly BindableProperty ClassTileCommandProperty = BindableProperty.Create(nameof(ClassTileCommand), typeof(ICommand), typeof(ClassTile), default(ICommand));
        public Class ClassData
        {
            get {  return (Class)GetValue(ClassDataProperty);}
            set { SetValue(ClassDataProperty, value);}
        }

        public ICommand ClassTileCommand
        {
            get { return (ICommand)GetValue(ClassTileCommandProperty); }
            set { SetValue(ClassTileCommandProperty, value);}
        }

        public ClassTile()
        {
            schoolDatabase = new SchoolDatabase();
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


            var classTitle = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 18,
                Margin = 5
            };
            classTitle.SetBinding(Label.TextProperty, new Binding("ClassData.ClassName", source: this));
            Grid.SetRow(classTitle, 0);
            Grid.SetColumn(classTitle, 0);
            grid.Children.Add(classTitle);

            Content = grid;

            var tapGester = new TapGestureRecognizer();
            tapGester.Tapped += OnTileTapped; 
            this.GestureRecognizers.Add(tapGester);
        }

        public void OnTileTapped(object sender, EventArgs e)
        {
            ClassTileCommand?.Execute(ClassData.Id);
        }


    }
}
