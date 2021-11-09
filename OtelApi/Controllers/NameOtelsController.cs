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
using OtelApi;
using OtelApi.GlobalEntity;

namespace OtelApi.Controllers
{
    public class NameOtelsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/NameOtels
        public IQueryable<NameOtel> GetNameOtel()
        {
            return db.NameOtel;
        }


        // GET: api/NameOtels/5
        [ResponseType(typeof(NameOtel))]
        public IHttpActionResult GetNameOtel(int id)
        {
            NameOtel nameOtel = db.NameOtel.Find(id);
            if (nameOtel == null)
            {
                return NotFound();
            }

            return Ok(nameOtel);
        }

        // PUT: api/NameOtels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNameOtel(int id, NameOtel nameOtel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nameOtel.ID)
            {
                return BadRequest();
            }

            db.Entry(nameOtel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NameOtelExists(id))
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

        // POST: api/NameOtels
        [ResponseType(typeof(NameOtel))]
        public IHttpActionResult PostNameOtel(NameOtel nameOtel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NameOtel.Add(nameOtel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NameOtelExists(nameOtel.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nameOtel.ID }, nameOtel);
        }

        // DELETE: api/NameOtels/5
        [ResponseType(typeof(NameOtel))]
        public IHttpActionResult DeleteNameOtel(int id)
        {
            NameOtel nameOtel = db.NameOtel.Find(id);
            if (nameOtel == null)
            {
                return NotFound();
            }

            db.NameOtel.Remove(nameOtel);
            db.SaveChanges();

            return Ok(nameOtel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NameOtelExists(int id)
        {
            return db.NameOtel.Count(e => e.ID == id) > 0;
        }
    }
}