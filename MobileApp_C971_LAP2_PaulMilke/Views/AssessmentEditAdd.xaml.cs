using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke.Views;


public partial class AssessmentEditAdd : ContentPage
{
	AssessmentEditAddViewModel ViewModel => (AssessmentEditAddViewModel)BindingContext; 
	public AssessmentEditAdd(AssessmentEditAddViewModel viewModel)
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