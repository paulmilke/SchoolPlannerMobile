using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke;

public partial class CoursesPage : ContentPage
{
	CoursesViewModel ViewModel => (CoursesViewModel)BindingContext; 
	public CoursesPage(CoursesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await ViewModel.OnNavigatedToAsync(); 
    }
}