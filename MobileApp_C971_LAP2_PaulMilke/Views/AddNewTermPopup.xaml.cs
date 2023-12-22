using CommunityToolkit.Maui.Views;
using MobileApp_C971_LAP2_PaulMilke.View_Model;

namespace MobileApp_C971_LAP2_PaulMilke.Views;

public partial class AddNewTermPopup : Popup
{
	public AddNewTermPopup(AddNewTermPopupViewModel viewModel)
	{
        InitializeComponent();
		this.BindingContext = viewModel;
	}

	public void AddButtonClicked(object sender, EventArgs e)
	{
		this.CloseAsync(); 
	}



}