using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WeatherApp.BusinessLogic;
using WeatherApp.Data;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace WeatherAppUnitTests
{
    [TestClass]
    public class WeatherLogicTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //TODO Assemble Test data
            var germany = new Country()
            {
                Name = "Germany"
            };

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

            var mockRecords = new List<WeatherRecord>();
            for (int i = 0; i < 100; i++)
            {
                var r1 = new WeatherRecord()
                {
                    Location = erlangen,
                    Summary = "Sunny",
                    Temperature = 10 + (i % 20),
                    Time = DateTimeOffset.Now.AddHours(i - 1)
                };
                mockRecords.Add(r1);

                var r2 = new WeatherRecord()
                {
                    Location = nuremberg,
                    Summary = "Cloudy",
                    Temperature = 7 + (i % 15),
                    Time = DateTimeOffset.Now.AddHours(i - 1)
                };
                mockRecords.Add(r2);
            }


            var context = Mock.Of<IWeatherRepository>();
            Mock.Get(context)
                .Setup(c => c.Countries)
                .Returns(new List<Country> { germany });
            Mock.Get(context)
                .Setup(c => c.Locations)
                .Returns(new List<Location> { erlangen, nuremberg });

            Mock.Get(context)
                .Setup(c => c.WeatherRecords)
                .Returns(mockRecords);

            var bussinesLogic = new WeatherLogic(context);

            var currentWeather = bussinesLogic.WeatherForLocation("germany", "erlangen");

            Assert.IsNotNull(currentWeather);
            Assert.AreEqual(currentWeather.Location, erlangen);
            Assert.AreEqual(currentWeather.Location.Country, germany);
            Assert.AreEqual(currentWeather.Summary, "Sunny");

        }

        //TODO Add Test Methods for every use case for every function
    }

}