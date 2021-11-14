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
    public class OtelsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/Otels
        public IQueryable<Otel> GetOtel()
        {
            return db.Otel;
        }

        // GET: api/Otels/order
        [Route("api/Otels/order")]
        [ResponseType(typeof(Otel))]
        public IHttpActionResult GetOtelByOrderID(int id)
        {
            var otel = (from o in db.Otel
                        join r in db.Order on o.ID equals r.OtelID
                        where r.ID == id
                        select o).Distinct();

            if (otel == null)
            {
                return NotFound();
            }

            return Ok(otel);
        }

        // GET: api/Otels/coutry
        [Route("api/Otels/country")]
        [ResponseType(typeof(Otel))]
        public IHttpActionResult GetOtelByCountryID(int id)
        {
            var otel = db.Otel.Where(e => e.AddressOfOtel.Country.ID == id);

            if (otel == null)
            {
                return NotFound();
            }

            return Ok(otel);
        }

        // GET: api/Otels/5
        [ResponseType(typeof(Otel))]
        public IHttpActionResult GetOtel(int id)
        {
            Otel otel = db.Otel.Find(id);
            if (otel == null)
            {
                return NotFound();
            }

            return Ok(otel);
        }

        // PUT: api/Otels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOtel(int id, Otel otel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != otel.ID)
            {
                return BadRequest();
            }

            db.Entry(otel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtelExists(id))
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

        // POST: api/Otels
        [ResponseType(typeof(Otel))]
        public IHttpActionResult PostOtel(Otel otel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Otel.Add(otel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OtelExists(otel.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = otel.ID }, otel);
        }

        // DELETE: api/Otels/5
        [ResponseType(typeof(Otel))]
        public IHttpActionResult DeleteOtel(int id)
        {
            Otel otel = db.Otel.Find(id);
            if (otel == null)
            {
                return NotFound();
            }

            db.Otel.Remove(otel);
            db.SaveChanges();

            return Ok(otel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OtelExists(int id)
        {
            return db.Otel.Count(e => e.ID == id) > 0;
        }
    }
}