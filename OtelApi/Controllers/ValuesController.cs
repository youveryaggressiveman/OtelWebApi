using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OtelApi.GlobalEntity;

namespace OtelApi.Controllers
{
    public class ValuesController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/Values
        public IQueryable<Value> GetValue()
        {
            return db.Value;
        }

        [Route("api/Values/priceId")]
        [ResponseType(typeof(Value))]
        public IQueryable<Value> GetValuebyPriceId(int id)
        {
            var result = (from c in db.Value
                          join o in db.Price on c.ID equals o.ValueID
                          where o.ID == id
                          select c).Distinct();
            return result;
        }

        // GET: api/Values/5
        [ResponseType(typeof(Value))]
        public IHttpActionResult GetValue(int id)
        {
            Value value = db.Value.Find(id);
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        // PUT: api/Values/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutValue(int id, Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.ID)
            {
                return BadRequest();
            }

            db.Entry(value).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Values
        [ResponseType(typeof(Value))]
        public IHttpActionResult PostValue(Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Value.Add(value);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = value.ID }, value);
        }

        // DELETE: api/Values/5
        [ResponseType(typeof(Value))]
        public IHttpActionResult DeleteValue(int id)
        {
            Value value = db.Value.Find(id);
            if (value == null)
            {
                return NotFound();
            }

            db.Value.Remove(value);
            db.SaveChanges();

            return Ok(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ValueExists(int id)
        {
            return db.Value.Count(e => e.ID == id) > 0;
        }
    }
}