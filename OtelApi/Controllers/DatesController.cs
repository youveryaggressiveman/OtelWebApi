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
    public class DatesController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/Dates
        public IQueryable<Date> GetDate()
        {
            return db.Date;
        }

        // GET: api/Dates/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult GetDate(int id)
        {
            Date date = db.Date.Find(id);
            if (date == null)
            {
                return NotFound();
            }

            return Ok(date);
        }

        // PUT: api/Dates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDate(int id, Date date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != date.ID)
            {
                return BadRequest();
            }

            db.Entry(date).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateExists(id))
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

        // POST: api/Dates
        [ResponseType(typeof(Date))]
        public IHttpActionResult PostDate(Date date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Date.Add(date);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = date.ID }, date);
        }

        // DELETE: api/Dates/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult DeleteDate(int id)
        {
            Date date = db.Date.Find(id);
            if (date == null)
            {
                return NotFound();
            }

            db.Date.Remove(date);
            db.SaveChanges();

            return Ok(date);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DateExists(int id)
        {
            return db.Date.Count(e => e.ID == id) > 0;
        }
    }
}