using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MobileApp_C971_LAP2_PaulMilke.Controls;
using MobileApp_C971_LAP2_PaulMilke.Models; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int ClassId { get; set; }

        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}

        public Assessment()
        {

        }

        public Assessment(int classId, string assessmentName, string assessmentType, DateTime startDate, DateTime endDate)
        {
            ClassId = classId;
            AssessmentName = assessmentName;
            AssessmentType = assessmentType;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class PerformanceAssessment : Assessment
    {
        public PerformanceAssessment() : base()
        {
            AssessmentType = "Performance Assessment"; 
        }
        public PerformanceAssessment(int classId, string assessmentName, DateTime startDate, DateTime endDate) : base(classId, assessmentName, "Performance Assessment", startDate, endDate) 
        {

        }
    }

    public class ObjectiveAssessment : Assessment
    {
        public ObjectiveAssessment() : base()
        {
            AssessmentType = "Objective Assessment"; 
        }
        public ObjectiveAssessment(int classId, string assessmentName, DateTime startDate, DateTime endDate) : base(classId, assessmentName, "Objective Assessment", startDate, endDate)
        {

        }
    }


}
