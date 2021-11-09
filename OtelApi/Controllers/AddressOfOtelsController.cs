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
    public class AddressOfOtelsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/AddressOfOtels
        public IQueryable<AddressOfOtel> GetAddressOfOtel()
        {
            return db.AddressOfOtel;
        }

        // GET: api/AddressOfOtels/5
        [ResponseType(typeof(AddressOfOtel))]
        public IHttpActionResult GetAddressOfOtel(int id)
        {
            AddressOfOtel addressOfOtel = db.AddressOfOtel.Find(id);
            if (addressOfOtel == null)
            {
                return NotFound();
            }

            return Ok(addressOfOtel);
        }

        // PUT: api/AddressOfOtels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddressOfOtel(int id, AddressOfOtel addressOfOtel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addressOfOtel.ID)
            {
                return BadRequest();
            }

            db.Entry(addressOfOtel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressOfOtelExists(id))
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

        // POST: api/AddressOfOtels
        [ResponseType(typeof(AddressOfOtel))]
        public IHttpActionResult PostAddressOfOtel(AddressOfOtel addressOfOtel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AddressOfOtel.Add(addressOfOtel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = addressOfOtel.ID }, addressOfOtel);
        }

        // DELETE: api/AddressOfOtels/5
        [ResponseType(typeof(AddressOfOtel))]
        public IHttpActionResult DeleteAddressOfOtel(int id)
        {
            AddressOfOtel addressOfOtel = db.AddressOfOtel.Find(id);
            if (addressOfOtel == null)
            {
                return NotFound();
            }

            db.AddressOfOtel.Remove(addressOfOtel);
            db.SaveChanges();

            return Ok(addressOfOtel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressOfOtelExists(int id)
        {
            return db.AddressOfOtel.Count(e => e.ID == id) > 0;
        }
    }
}