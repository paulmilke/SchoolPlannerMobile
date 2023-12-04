using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;


namespace MobileApp_C971_LAP2_PaulMilke.View_Model;

[QueryProperty(nameof(ClassID), "OBJECTID")]
public class EditCourseViewModel : BaseViewModel
{
	SchoolDatabase schoolDatabase; 
	private int classID; 
	public int ClassID
	{
		get => classID;
		set
		{
			classID = value;
			OnPropertyChanged();
        }
    }

    private bool isEditing;
    public bool IsEditing
    {
        get => isEditing;
        set
        {
            isEditing = value;
            OnPropertyChanged(nameof(IsEditing));
        }
    }

    public ICommand ToggleEditCommand { get; }
	public ICommand UpdateClassCommand { get; }
	public ICommand DeleteClassCommand { get; }

    private Class currentClass; 
	public Class CurrentClass
	{
		get { return currentClass; }
		set 
		{ 
			currentClass = value; 
			OnPropertyChanged(); 
			OnPropertyChanged(nameof(CourseName)); // This will refresh the CourseName binding
			OnPropertyChanged(nameof(Start)); 
			OnPropertyChanged(nameof(End));
		}
    }

	public EditCourseViewModel(INavigationService navigationService) : base(navigationService)
	{
		schoolDatabase = new SchoolDatabase();
        ToggleEditCommand = new Command(() => IsEditing = !IsEditing);
		UpdateClassCommand = new Command(async () => await UpdateClass());
		DeleteClassCommand = new Command(async() => await DeleteClass());
    }

	public async Task InitializeAsync()
	{
		await LoadClassInfo();
		UpdateBindedProperties();
	}

	public async Task LoadClassInfo()
	{
		CurrentClass = await schoolDatabase.GetSingleClass(ClassID);
	}

	public void UpdateBindedProperties()
	{
		CourseName = CurrentClass?.ClassName ?? "Loading...";
		Start = CurrentClass?.StartDate ?? DateTime.Today;
        End = CurrentClass?.EndDate ?? DateTime.Today;
		Status = CurrentClass?.Status ?? "Loading...";
		InstructorName = CurrentClass?.InstructorName ?? "Loading...";
		InstructorEmail = CurrentClass?.InstructorEmail ?? "Loading...";
		InstructorPhone = CurrentClass?.InstructorPhone ?? "Loading...";
    }

	public async Task UpdateClass()
	{
		CurrentClass.ClassName = CourseName;
		CurrentClass.StartDate = Start;
		CurrentClass.EndDate = End; 
		CurrentClass.Status = Status;
		CurrentClass.InstructorName = InstructorName;
		CurrentClass.InstructorEmail = InstructorEmail;
		CurrentClass.InstructorPhone = InstructorPhone;
		await schoolDatabase.SaveClassAsync(CurrentClass); 
		ToggleEditCommand.Execute(this);
	}

	public async Task DeleteClass()
	{

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.DarkRed,
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.White,
            CornerRadius = new CornerRadius(10),
        };

        var snackbar = Snackbar.Make("Are you sure you want to delete this item?", async() => 
			{
				await schoolDatabase.DeleteClassAsync(currentClass);
				await Shell.Current.Navigation.PopAsync(); 
			}, "Delete", TimeSpan.FromSeconds(5),
			snackbarOptions);
		await snackbar.Show();

    }



	private string courseName; 
	public string CourseName
	{
		get => courseName;
		set
		{
			courseName = value;
			OnPropertyChanged(nameof(CourseName));
		}
    }
	  
	private DateTime start; 
	public DateTime Start
	{
		get => start; 
		set
		{
			start = value; 
			OnPropertyChanged(nameof(Start));
		}
	}

	private DateTime end;
	public DateTime End
	{
		get => end;
		set
		{
			end = value;
			OnPropertyChanged(nameof(End));
		}
	}

	private string status; 
	public string Status
	{
		get => status; 
		set
		{
			status = value;
			OnPropertyChanged(nameof(Status));
		}
	}

	private string instructorName; 
	public string InstructorName
	{
		get => instructorName; 
		set
		{
			instructorName = value;
			OnPropertyChanged(nameof(InstructorName));
		}
	}

	private string instructorEmail; 
	public string InstructorEmail
	{
		get => instructorEmail; 
		set
		{
			instructorEmail = value;
			OnPropertyChanged(nameof(InstructorEmail));
		}
	}

	private string instructorPhone;
	public string InstructorPhone
	{
		get => instructorPhone; 
		set
		{
			instructorPhone = value;
			OnPropertyChanged(nameof(InstructorPhone));
		}
	}
}