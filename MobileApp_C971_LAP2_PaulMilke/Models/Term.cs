using SQLite;
using System.Text.Json.Serialization;

namespace MobileApp_C971_LAP2_PaulMilke.Models
{
    public class Term 
    {
        [PrimaryKey, AutoIncrement]
        [JsonPropertyName("termId")]
        public int Id { get; set; }
        [JsonPropertyName("termName")]
        public string Title { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime Start { get; set; }
        [JsonPropertyName("endDate")]
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
