using api.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using api;
using Microsoft.Extensions.Logging.Abstractions;

namespace tests {
    public class ValuesControllerSpec{
        private readonly WeatherForecastController sut;

        public ValuesControllerSpec()
        {
            sut = new WeatherForecastController(new NullLogger<WeatherForecastController>());
        }

        [Fact]
        public void get_all_values_returns_ok_result()
        {
            var okResult = sut.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void get_all_values_returns_3_items()
        {
            var result = sut.Get().Result as OkObjectResult;
            var items = Assert.IsType<WeatherForecast[]>(result.Value);
            Assert.Equal(5, items.Count());
        }
    }
}