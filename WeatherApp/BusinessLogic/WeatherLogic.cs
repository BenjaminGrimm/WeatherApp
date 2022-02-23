using WeatherApp.Data;
using WeatherApp.Exceptions;

namespace WeatherApp.BusinessLogic
{
    public class WeatherLogic : IWeatherLogic
    {
        private readonly IWeatherDbContext _context;

        public WeatherLogic(IWeatherDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WeatherRecord> CountryOverview(string country)
        {
            var locations = GetCountryLocations(country);
            return _context.WeatherRecords.Where(w => locations.Contains(w.Location))
                .GroupBy(w => w.Location.Name)
                .Select(g => g.Where(w => w.Time <= DateTimeOffset.Now)
                                .OrderByDescending(w => w.Time)
                                .FirstOrDefault()
                )!;

        }

        public WeatherRecord WeatherForLocation(string country, string location)
        {
            return WeatherAtTime(country, location, DateTimeOffset.Now);
        }

        public IEnumerable<WeatherRecord> ThreeDayForecastForLocation(string country, string location)
        {
            var records = new List<WeatherRecord>();

            for (int i = 0; i < 3 * 24; i++)
            {
                var weatherRecord = WeatherAtTime(country, location, DateTimeOffset.Now.AddHours(i));
                if(weatherRecord != null)
                {
                    records.Add(weatherRecord);
                }
            }
            return records;
        }


        /// <summary>
        /// Finds the weather at a certain time and place.
        /// The correct record/prediction is connsidered to be the latest one at or before the specified time
        /// </summary>
        private WeatherRecord WeatherAtTime(string countryName, string locationName, DateTimeOffset time)
        {
            var location = GetLocation(countryName, locationName);
            var weatherRecord =  _context.WeatherRecords.Where(w => w.Location == location)
                .Where(w => w.Time <= time)
                .OrderByDescending(w => w.Time)
                .FirstOrDefault();

            if(weatherRecord == null)
            {
                throw new NoRecordsFoundException();
            }
            
            return weatherRecord;
        }

       

        private Location GetLocation(string countryName, string locationName)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Name.Equals(countryName, StringComparison.OrdinalIgnoreCase));
            if(null == country) {
                throw new CountryNotFoundException();
            }

            var location = _context.Locations.FirstOrDefault(l => l.Name.Equals(locationName) && l.Country == country);
            if (null == location)
            {
                throw new LocationNotFoundException();
            }
            return location;
        }

        private IEnumerable<Location> GetCountryLocations(string countryName)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Name.Equals(countryName, StringComparison.OrdinalIgnoreCase));
            if (null == country)
            {
                throw new CountryNotFoundException();
            }
            var location = _context.Locations.Where(l => l.Country == country);

            return location;
        }

    }
}
