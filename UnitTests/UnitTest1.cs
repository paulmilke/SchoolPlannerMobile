using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;
using Moq;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void ValidateEntryValues_BlankClassName()
        {
            // Arrange
            var mockNavigationService = new Mock<INavigationService>();
            var mockNotificationService = new Mock<INotificationService>();
            var viewModel = new EditCourseViewModel(mockNavigationService.Object, mockNotificationService.Object);
            viewModel.CourseName = "";
            viewModel.Start = DateTime.Today;
            viewModel.End = DateTime.Today.AddDays(1);
            viewModel.Status = "Completed";
            viewModel.InstructorEmail = "myemail@gmail.com";
            viewModel.InstructorName = "Name";
            viewModel.InstructorPhone = "1234567";

            // Act
            var result = viewModel.ValidateEntryValues();

            // Assert
            var expectedAlert = "the title";
            Assert.False(result);
            Assert.Equal(expectedAlert, viewModel.AlertMessage);
        }

        [Fact]
        public void ValidateEntryValues_IncorrectDates()
        {
            // Arrange
            var mockNavigationService = new Mock<INavigationService>();
            var mockNotificationService = new Mock<INotificationService>();
            var viewModel = new EditCourseViewModel(mockNavigationService.Object, mockNotificationService.Object);
            viewModel.CourseName = "Name";
            viewModel.Start = DateTime.Today;
            viewModel.End = DateTime.Today.AddDays(-1);
            viewModel.Status = "Completed";
            viewModel.InstructorEmail = "myemail@gmail.com";
            viewModel.InstructorName = "Name";
            viewModel.InstructorPhone = "1234567";

            // Act
            var result = viewModel.ValidateEntryValues();

            // Assert
            var expectedAlert = "start and end dates";
            Assert.False(result);
            Assert.Equal(expectedAlert, viewModel.AlertMessage);
        }

        [Fact]
        public void ValidateEntryValues_NullStatus()
        {
            // Arrange
            var mockNavigationService = new Mock<INavigationService>();
            var mockNotificationService = new Mock<INotificationService>();
            var viewModel = new EditCourseViewModel(mockNavigationService.Object, mockNotificationService.Object);
            viewModel.CourseName = "Name";
            viewModel.Start = DateTime.Today;
            viewModel.End = DateTime.Today.AddDays(1);
            viewModel.Status = "";
            viewModel.InstructorEmail = "myemail@gmail.com";
            viewModel.InstructorName = "Name";
            viewModel.InstructorPhone = "1234567";

            // Act
            var result = viewModel.ValidateEntryValues();

            // Assert
            var expectedAlert = "course status";
            Assert.False(result);
            Assert.Equal(expectedAlert, viewModel.AlertMessage);
        }

        [Fact]
        public void ValidateEntryValues_NullInstructorData()
        {
            // Arrange
            var mockNavigationService = new Mock<INavigationService>();
            var mockNotificationService = new Mock<INotificationService>();
            var viewModel = new EditCourseViewModel(mockNavigationService.Object, mockNotificationService.Object);
            viewModel.CourseName = "Name";
            viewModel.Start = DateTime.Today;
            viewModel.End = DateTime.Today.AddDays(1);
            viewModel.Status = "Completed";
            viewModel.InstructorEmail = "mymail@gmail.com";
            viewModel.InstructorName = "";
            viewModel.InstructorPhone = "";

            // Act
            var result = viewModel.ValidateEntryValues();

            // Assert
            var expectedAlert = "instructor information"; 
            Assert.False(result);
            Assert.Equal(expectedAlert, viewModel.AlertMessage);
        }

        [Fact]
        public void ValidateEmail_IncorrectEmail()
        {
            //Arrange
            var mockNavigationService = new Mock<INavigationService>();
            var mockNotificationService = new Mock<INotificationService>();
            var viewModel = new EditCourseViewModel(mockNavigationService.Object, mockNotificationService.Object);

            viewModel.InstructorEmail = "mygmail.com";

            //Act
            var result = viewModel.ValidateEmail(); 

            //Assert

            Assert.False(result);

        }
    }
}