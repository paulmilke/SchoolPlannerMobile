using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Models
{
    public class Class
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int TermId { get; set; }
        
        public string ClassName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }

        public Class()
        {

        }

        public Class(int termID, string className)
        {
            TermId = termID; 
            ClassName = className;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public Class(int id, int termId, string className, DateTime startDate, DateTime endDate, string status, string instructorName, string instructorPhone, string instructorEmail, string notes)
        {
            Id = id;
            TermId = termId;
            ClassName = className;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            InstructorName = instructorName;
            InstructorPhone = instructorPhone;
            InstructorEmail = instructorEmail;
            Notes = notes;
        }
    }
}
