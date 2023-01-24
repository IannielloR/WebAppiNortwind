using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiPubs.Models;
using System.Linq;
using System.Security.Policy;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PubsContext context;

        public PublishersController(PubsContext context)
        {
            this.context = context;
        }
        // Get
        [HttpGet]
        public ActionResult<IEnumerable<Publishers>> GetClinica()
        {
            return context.Publishers.ToList();
        }
        //Get por Id
        [HttpGet("{id}")]
        public ActionResult<Publishers> GetByID(string id)
        {
            Publishers publisher = (from p in context.Publishers
                                    where id == p.PubId
                                 select p).SingleOrDefault();
            return publisher;
        }
        //UPDATE
        //PUT api/autor/{id}
        [HttpPut("{id}")]
        public ActionResult Put(string id, Publishers publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            context.Entry(publisher).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //INSERT
        [HttpPost]
        public ActionResult Post(Publishers publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }

        //DELETE
        //DELETE api/autor/{id}
        [HttpDelete("{id}")]
        public ActionResult<Publishers> Delete(string id)
        {
            var publisher = (from p in context.Publishers
                            where p.PubId == id
                            select p).SingleOrDefault();

            if (publisher == null)
            {
                return NotFound();
            }

            context.Publishers.Remove(publisher);
            context.SaveChanges();

            return publisher;

        }
    }
}
