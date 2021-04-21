using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.AppServices;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    public class ContactUsController : ControllerBase
    {
        private readonly ContactUsService _contactUsService;

        public ContactUsController(ContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ContactUs> Get()
        {
            return _contactUsService.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ContactUsForUpdate model)
        {
            _contactUsService.Update(model);
        }
    }
}
