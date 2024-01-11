using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.ApplicationModel.Communication;
using MobileApp_C971_LAP2_PaulMilke.Models;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.Views;
using Plugin.LocalNotification;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Input;


namespace MobileApp_C971_LAP2_PaulMilke.View_Model;

[QueryProperty(nameof(ClassID), "OBJECTID")]
public class EditCourseViewModel : BaseViewModel
{
	SchoolDatabase schoolDatabase; 
	private readonly Services.INotificationService notificationService;

	public ObservableCollection<AssessmentTile> AssessmentTiles { get; set; } = new ObservableCollection<AssessmentTile>();

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

	private bool isPerformance;
	public bool IsPerformance
	{
		get { return isPerformance; } 
		set 
		{
			isPerformance = value;
			OnPropertyChanged();
		}
	}

	private bool isObjective; 
	public bool IsObjective
	{
		get { return isObjective; }
		set
		{
			isObjective = value;
			OnPropertyChanged();
		}
	}

    public ICommand ToggleEditCommand { get; }
	public ICommand UpdateClassCommand { get; }
	public ICommand DeleteClassCommand { get; }
	public ICommand ShareNotesCommand { get; }
	public ICommand NavigateToPerformanceAssessment { get; }
    public ICommand NavigateToObjectiveAssessment { get; }
	public ICommand NavigateFromTile { get; }

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

	public EditCourseViewModel(INavigationService _navigationService, Services.INotificationService _notificationService) : base(_navigationService)
	{
		schoolDatabase = new SchoolDatabase();
		notificationService = _notificationService;

        ToggleEditCommand = new Command(() => IsEditing = !IsEditing);
		UpdateClassCommand = new Command(async () => await UpdateClass());
		DeleteClassCommand = new Command(async() => await DeleteClass());
		ShareNotesCommand = new Command(async () => await ShareNotes(CurrentClass.Notes));
		NavigateToPerformanceAssessment = new Command(async () => await NavigateToAssessmentPerformance());
        NavigateToObjectiveAssessment = new Command(async () => await NavigateToAssessmentObjective());
		NavigateFromTile = new Command<int>(async (assessmentID) => await NavigateFromTiletoEdit(assessmentID));
    }

	public async Task NavigateFromTiletoEdit(int assessmentID)
	{
		await NavigationService.NavigateToAsync(nameof(AssessmentEditAdd), assessmentID);
    }

	public async Task NavigateToAssessmentPerformance()
	{
		string assessment = "Performance Assessment"; 
		await NavigationService.NavigateToAsync(nameof(AssessmentEditAdd), assessment, true, ClassID);
    }

    public async Task NavigateToAssessmentObjective()
    {
        string assessment = "Objective Assessment";
		await NavigationService.NavigateToAsync(nameof(AssessmentEditAdd), assessment, true, ClassID);
    }


    public async Task InitializeAsync()
	{
		await LoadClassInfo();

	}

	public async Task LoadClassInfo()
	{
		AssessmentTiles.Clear();
		CurrentClass = await schoolDatabase.GetSingleClass(ClassID);
        UpdateBindedProperties();

        var list = await schoolDatabase.GetAssessmentsAsync(ClassID);

        IsPerformance = true;
        IsObjective = true;

        foreach (var item in list)
		{
			AssessmentTile newTile = new AssessmentTile { AssessmentData = item };
			AssessmentTiles.Add(newTile);


			if (item.AssessmentType == "Performance Assessment")
			{
				IsPerformance = false;
			}
			else if(item.AssessmentType == "Objective Assessment")
			{
				IsObjective = false;
			}
		}
	}

	public void UpdateBindedProperties()
	{
		CourseName = CurrentClass?.ClassName;
		Start = CurrentClass?.StartDate ?? DateTime.Today;
        End = CurrentClass?.EndDate ?? DateTime.Today;
		Status = CurrentClass?.Status;
		InstructorName = CurrentClass?.InstructorName;
		InstructorEmail = CurrentClass?.InstructorEmail;
		InstructorPhone = CurrentClass?.InstructorPhone;
		ClassNotes = CurrentClass?.Notes;

		if(CourseName == "New Class") 
		{
			IsEditing = true;
		}
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
        CurrentClass.Notes = ClassNotes;

        if (ValidateEntryValues() == true)
		{

            await schoolDatabase.SaveClassAsync(CurrentClass);
            ToggleEditCommand.Execute(this);
            await ScheduleClassNotificationAsync();
        }
		else
		{
			DisplayAlert();
		}

	}

	public bool ValidateEntryValues()
	{
		if (string.IsNullOrWhiteSpace(CourseName))
        {
			AlertMessage = "the title";
			return false; 
		}
        else if (CourseName == "New Class")
		{
			App.Current.MainPage.DisplayAlert("Alert", "Please change course name to something other than 'New Class'", "Okay");
			return false; 
		}
		else if (Start.Date == End.Date || Start.Date > End.Date)
		{
			AlertMessage = "start and end dates";
			return false; 
		}
		else if (Status == null)
		{
			AlertMessage = "course status";
			return false; 
		}
		else if(InstructorEmail == null || InstructorName == null || InstructorPhone == null) 
		{
			AlertMessage = "instructor information";
			return false; 
		}
		else if(ValidateEmail() == false)
		{
			AlertMessage = "instructor email";
			return false; 
		}
		else
		{
			return true; 
		}

	}

	public bool ValidateEmail()
	{
        try
        {
            return Regex.IsMatch(InstructorEmail,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

	private async void DisplayAlert()
	{
		await App.Current.MainPage.DisplayAlert("Alert", $"Please check {AlertMessage} for errors.", "Okay");
	}

	public async Task ScheduleClassNotificationAsync()
	{
		if (await notificationService.AreNotificationsEnabledAsync())
		{
			//Start date notification
			var time = DateTime.Now;
			if (CurrentClass.StartDate == DateTime.Today)
			{
				time = DateTime.Now.AddSeconds(1);
			}
			else
			{
				time = CurrentClass.StartDate.AddHours(8); 
			}

            var notificationRequest = new NotificationRequest
            {
                NotificationId = int.Parse($"{CurrentClass.Id}1"),
                Title = "Course Starting",
                Description = $"Your course: {CurrentClass.ClassName} starts today!",
                ReturningData = "Dummy Data",
                Schedule =
            {
                NotifyTime =  time,
            }
            };

			//End date notification
			var endTime = CurrentClass.EndDate.AddHours(8);

			if(CurrentClass.EndDate == DateTime.Today)
			{
				endTime = DateTime.Now.AddSeconds(1);
			}
			else
			{
				endTime = CurrentClass.EndDate.AddHours(8);
			}

			var endNotificationRequest = new NotificationRequest
			{
				NotificationId = int.Parse($"{CurrentClass.Id}2"),
				Title = "Course Ending",
				Description = $"Your course: {CurrentClass.ClassName} ends today!",
				ReturningData = "Dummy Data", 
				Schedule =
				{
					NotifyTime = endTime,
				}
			};

			//Scheduling both notifications. 
            await notificationService.ScheduleNotificationAsync(notificationRequest);
			await notificationService.ScheduleNotificationAsync(endNotificationRequest);
        }
		else
		{
			await notificationService.RequestNotificationPermissionAsync(); 
		}

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

	public async Task ShareNotes(string text)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Share Notes"
        });
    }

	private string alertMessage;
	public string AlertMessage
	{
		get => alertMessage;
		set
		{
			alertMessage = value;
			OnPropertyChanged(); 
		}
	}

	//Properties for the course in question. 
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

	private string classNotes; 
	public string ClassNotes
	{
		get => classNotes;
		set
		{
			classNotes = value;
			OnPropertyChanged(nameof(ClassNotes));
		}
	}
}