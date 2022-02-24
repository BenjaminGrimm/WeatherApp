namespace WeatherApp.Data
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext weatherDbContext;

        public WeatherRepository(WeatherDbContext weatherDbContext)
        {
            this.weatherDbContext = weatherDbContext;
        }

        public IEnumerable<Country> Countries => this.weatherDbContext.Countries;

        public IEnumerable<Location> Locations => this.weatherDbContext.Locations;

        public IEnumerable<WeatherRecord> WeatherRecords => this.weatherDbContext.WeatherRecords;

        //TODO Implement write data
    }
}
