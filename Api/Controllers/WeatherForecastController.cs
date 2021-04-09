using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILoggerManager _logger;

        public WeatherForecastController(IRepositoryWrapper repoWrapper,ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable Get()
        {
            var products = _repoWrapper.Product.FindAll().AsEnumerable();
            var lists = products.ToList();

            return products.Select(x => new { x.Code, x.Name });
        }
    }
}
