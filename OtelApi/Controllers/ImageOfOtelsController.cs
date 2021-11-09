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
    public class ImageOfOtelsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/ImageOfOtels
        public IQueryable<ImageOfOtel> GetImageOfOtel()
        {
            return db.ImageOfOtel;
        }

        // GET: api/ImageOfOtels/5
        [ResponseType(typeof(ImageOfOtel))]
        public IHttpActionResult GetImageOfOtel(int id)
        {
            ImageOfOtel imageOfOtel = db.ImageOfOtel.Find(id);
            if (imageOfOtel == null)
            {
                return NotFound();
            }

            return Ok(imageOfOtel);
        }

        // PUT: api/ImageOfOtels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImageOfOtel(int id, ImageOfOtel imageOfOtel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imageOfOtel.ID)
            {
                return BadRequest();
            }

            db.Entry(imageOfOtel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageOfOtelExists(id))
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

        // POST: api/ImageOfOtels
        [ResponseType(typeof(ImageOfOtel))]
        public IHttpActionResult PostImageOfOtel(ImageOfOtel imageOfOtel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ImageOfOtel.Add(imageOfOtel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ImageOfOtelExists(imageOfOtel.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = imageOfOtel.ID }, imageOfOtel);
        }

        // DELETE: api/ImageOfOtels/5
        [ResponseType(typeof(ImageOfOtel))]
        public IHttpActionResult DeleteImageOfOtel(int id)
        {
            ImageOfOtel imageOfOtel = db.ImageOfOtel.Find(id);
            if (imageOfOtel == null)
            {
                return NotFound();
            }

            db.ImageOfOtel.Remove(imageOfOtel);
            db.SaveChanges();

            return Ok(imageOfOtel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageOfOtelExists(int id)
        {
            return db.ImageOfOtel.Count(e => e.ID == id) > 0;
        }
    }
}