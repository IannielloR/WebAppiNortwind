using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase

    {
        private readonly PubsContext context;

        public StoresController(PubsContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Stores>> Get()
        {
            return context.Stores.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Stores> GetById(string id)
        {
            Stores store = (from s in context.Stores
                            where s.StorId== id
                            select s).SingleOrDefault();
            return store;
        }

        [HttpGet("name/{name}")]
        public ActionResult<Stores> GetByName(string name)
        {
            Stores store =(from s in context.Stores
                           where s.StorName== name
                           select s).SingleOrDefault();

            return store;
        }

        [HttpGet("zip/{zip}")]
        public ActionResult<IEnumerable<Stores>> GetByZip(string zip)
        {
            List<Stores> store = (from s in context.Stores
                                  where s.StorId== zip
                                  select s).ToList();
            if(store.Count == 0)
            {
                return NotFound();
            }
            return store;

        }

        [HttpGet("{city}/{state}")]
        public ActionResult<IEnumerable<Stores>> GetByCityState(string city, string state)
        {
            List<Stores> store = (from s in context.Stores
                                  where s.City == city && s.State == state
                                  select s).ToList();
            if (store.Count == 0)
            {
                return NotFound();
            }
            return store;
        }

        //UPDATE
        //PUT api/autor/{id}
        [HttpPut("{id}")]
        public ActionResult Put(string id, Stores store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }

            context.Entry(store).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //INSERT
        [HttpPost]
        public ActionResult Post(Stores store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Stores.Add(store);
            context.SaveChanges();
            return Ok();
        }

        //DELETE
        //DELETE api/autor/{id}
        [HttpDelete("{id}")]
        public ActionResult<Stores> Delete(string id)
        {
            var store = (from s in context.Stores
                             where s.StorId == id
                             select s).SingleOrDefault();

            if (store == null)
            {
                return NotFound();
            }

            context.Stores.Remove(store);
            context.SaveChanges();

            return store;

        }

    }
}
