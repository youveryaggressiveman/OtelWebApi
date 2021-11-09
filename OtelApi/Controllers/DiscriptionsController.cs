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
    public class DiscriptionsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/Discriptions
        public IQueryable<Discription> GetDiscription()
        {
            return db.Discription;
        }

        // GET: api/Discriptions/5
        [ResponseType(typeof(Discription))]
        public IHttpActionResult GetDiscription(int id)
        {
            Discription discription = db.Discription.Find(id);
            if (discription == null)
            {
                return NotFound();
            }

            return Ok(discription);
        }

        // PUT: api/Discriptions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscription(int id, Discription discription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discription.ID)
            {
                return BadRequest();
            }

            db.Entry(discription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscriptionExists(id))
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

        // POST: api/Discriptions
        [ResponseType(typeof(Discription))]
        public IHttpActionResult PostDiscription(Discription discription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discription.Add(discription);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DiscriptionExists(discription.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = discription.ID }, discription);
        }

        // DELETE: api/Discriptions/5
        [ResponseType(typeof(Discription))]
        public IHttpActionResult DeleteDiscription(int id)
        {
            Discription discription = db.Discription.Find(id);
            if (discription == null)
            {
                return NotFound();
            }

            db.Discription.Remove(discription);
            db.SaveChanges();

            return Ok(discription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscriptionExists(int id)
        {
            return db.Discription.Count(e => e.ID == id) > 0;
        }
    }
}