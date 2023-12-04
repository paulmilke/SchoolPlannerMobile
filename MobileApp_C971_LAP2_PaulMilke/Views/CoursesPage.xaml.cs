using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke;

public partial class CoursesPage : ContentPage
{
	public CoursesPage(CoursesViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;

	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as CoursesViewModel;
        if (viewModel != null)
        {
            await viewModel.InitializeAsync();
        }
    }
}