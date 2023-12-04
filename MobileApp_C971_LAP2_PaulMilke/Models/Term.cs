using SQLite;

namespace MobileApp_C971_LAP2_PaulMilke.Models
{
    public class Term 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Term() 
        {
        }

        public Term (string title, DateTime startDate, DateTime endDate)
        {
            Title = title;
            Start = startDate;
            End = endDate;
        }
    }
}
