using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Data
{
    public class WeatherRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Location Location { get; set; }

        public double Temperature { get; set; }

        public string? Summary { get; set; }

        public DateTimeOffset Time {get; set;} 
    }
}
