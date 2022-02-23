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
            var nuernberg = new Location()
            {
                Country = germany,
                Name = "Nuernberg"
            };

            for (int i = 0; i < 100; i++)
            {
                var r1 = new WeatherRecord()
                {
                    Location = erlangen,
                    Summary = "Sunny",
                    Temperature = 10 + (i % 20),
                    Time = DateTimeOffset.Now.AddHours(i - 1)
                };

                var r2 = new WeatherRecord()
                {
                    Location = nuernberg,
                    Summary = "Cloudy",
                    Temperature = 7 + (i % 15),
                    Time = DateTimeOffset.Now.AddHours(i - 1)
                };
            }


            var context = Mock.Of<IWeatherDbContext>();
            Mock.Get(context)
                .Setup(c => c.Countries)
                .Returns(new List<Country> { germany }); //TODO Does not work this way. The DBSet needs to be mocked too.

            var bussinesLogic = new WeatherLogic(context);

            var currentWeather = bussinesLogic.WeatherForLocation("germany", "erlangen");

            Assert.IsNotNull(currentWeather);

        }

        //TODO Add Test Methods for every use case for every function
    }

}