using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke.Views;


public partial class AssessmentEditAdd : ContentPage
{
	public AssessmentEditAdd(AssessmentEditAddViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as AssessmentEditAddViewModel;
        if (viewModel != null)
        {
            viewModel.InitializeAsync();
        }
    }

}