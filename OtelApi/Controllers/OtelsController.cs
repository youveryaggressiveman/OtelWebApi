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
    
    public class OtelsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/Otels
        public IQueryable<Otel> GetOtel()
        {
            return db.Otel;
        }

        [Route("api/Otels/country")]
        [ResponseType(typeof(Otel))]
        public IQueryable<Otel> GetOtelByCountry(int id)
        {
            return db.Otel.Where(e => e.AddressOfOtel.Country.ID == id);

        }

        [Route("api/Otels/addressOfOtel")]
        [ResponseType(typeof(AddressOfOtel))]
        public IQueryable<AddressOfOtel> GetOtelbyAddressOfOtel(int otelID)
        {
            var result = from d in db.AddressOfOtel
                         join o in db.Otel on d.ID equals o.AddressOfOtelID
                         where o.ID == otelID
                         select d;
            return result;
        }

        [Route("api/Otels/description")]
        [ResponseType(typeof(Discription))]
        public IQueryable<Discription> GetOtelbyDescription(int otelID)
        {
            var result = from d in db.Discription
                         join o in db.Otel on d.ID equals o.DiscriptionID
                         where o.ID == otelID
                         select d;
            return result;
        }

        
        [Route("api/Otels/image")]
        [ResponseType(typeof(ImageOfOtel))]
        public IQueryable<ImageOfOtel> GetOtelbyImageOfOtel(int otelID)
        {
            var result = from d in db.ImageOfOtel
                         join o in db.Otel on d.ID equals o.ImageID
                         where o.ID == otelID
                         select d;
            return result;
        }

        [Route("api/Otels/name")]
        [ResponseType(typeof(Otel))]
        public Otel GetOtelbyName(string otelName)
        {
            var result = db.Otel.FirstOrDefault(e => e.NameOtel.Name == otelName);
            return result;
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