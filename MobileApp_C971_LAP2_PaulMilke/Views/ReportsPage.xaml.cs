using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke.Views;

public partial class ReportsPage : ContentPage
{
	public ReportsPage()
	{
		InitializeComponent();
		BindingContext = new ReportsViewModel(new NavigationService()); 
	}
}