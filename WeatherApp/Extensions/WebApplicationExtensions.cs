using WeatherApp.Data;

namespace WeatherApp.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication SeedWeatherData(this WebApplication webHost)
        {
            var scope = webHost.Services.GetService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
            SeedDbContext(context);


            return webHost;
        }

        private static void SeedDbContext(WeatherDbContext context)
        {
            var germany = new Country()
            {
                Name = "Germany"
            };
            context.Countries.Add(germany);

            var erlangen = new Location()
            {
                Country = germany,
                Name = "Erlangen"
            };
            var nuremberg = new Location()
            {
                Country = germany,
                Name = "Nuremberg"
            };

            context.Locations.Add(erlangen);
            context.Locations.Add(nuremberg);

            for (int i = 0; i < 100; i++)
            {
                var r1 = new WeatherRecord()
                {
                    Location = erlangen,
                    Summary = "Sunny",
                    Temperature = 10 + (i % 20),
                    Time = DateTimeOffset.Now.AddHours(i - 1)
                };
                context.WeatherRecords.Add(r1);

                var r2 = new WeatherRecord()
                {
                    Location = nuremberg,
                    Summary = "Cloudy",
                    Temperature = 7 + (i % 15),
                    Time = DateTimeOffset.Now.AddHours(i - 1)
                };
                context.WeatherRecords.Add(r2);
            }
            context.SaveChanges();
        }
    }
}
