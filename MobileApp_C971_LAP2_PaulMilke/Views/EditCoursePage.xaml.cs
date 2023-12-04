using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke.Views;

public partial class EditCoursePage : ContentPage
{
	public EditCoursePage(EditCourseViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as EditCourseViewModel;
        if (viewModel != null)
        {
            await ((EditCourseViewModel)BindingContext).InitializeAsync();
        }
    }
}