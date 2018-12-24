using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interface;

namespace MmFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public ValuesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET api/values
        [HttpGet]
        public List<Hero> Get()
        {
            return new List<Hero>()
            {
                new Hero()
                {
                    Id = 1,
                    Name = "Superman",
                },
                new Hero()
                {
                    Id = 11,
                    Name = "IronMan",
                },new Hero()
                {
                    Id = 12,
                    Name = "AntMan",
                }
            };
        }

        [HttpGet("get2")]
        public ActionResult<List<Villain>> Get2()
        {
            var t=new List<Villain>()
            {
                new Villain()
                {
                    Id = 1,
                    Name = "Superman",
                    AgainstHero = "Dog"
                },
                new Villain()
                {
                    Id = 11,
                    Name = "IronMan",
                    AgainstHero = "Dog"
                },new Villain()
                {
                    Id = 12,
                    Name = "AntMan",
                    AgainstHero = "Dog"
                }
            };
            return t;
        }

        public class Villain
        {
            public int Id;
            public string Name;
            public string AgainstHero;
        }
        public class Hero
        {
            public int Id;
            public string Name;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Hero value)
        {
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
