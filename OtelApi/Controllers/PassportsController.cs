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
    public class PassportsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/Passports
        public IQueryable<Passport> GetPassport()
        {
            return db.Passport;
        }

        [Route("api/serial")]
        [ResponseType(typeof(Passport))]
        public IQueryable<Passport> GetPassportByNumber(string serial)
        {
            return db.Passport.Where(e => e.PassportSerial == serial);
        }

        // GET: api/Passports/5
        [ResponseType(typeof(Passport))]
        public IHttpActionResult GetPassport(int id)
        {
            Passport passport = db.Passport.Find(id);
            if (passport == null)
            {
                return NotFound();
            }

            return Ok(passport);
        }

        // PUT: api/Passports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPassport(int id, Passport passport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != passport.ID)
            {
                return BadRequest();
            }

            db.Entry(passport).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassportExists(id))
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

        // POST: api/Passports
        [ResponseType(typeof(Passport))]
        public IHttpActionResult PostPassport(Passport passport)
        {
            if(passport != null)
            {
                if (passport.PassportSerial == null)
                {
                    ModelState.AddModelError("Passport", "Серия паспорта не может быть пустой");
                }

                if (passport.PassportNumber == null)
                {
                    ModelState.AddModelError("Passport", "Номер пасопрта не может быть пустым");
                }

            }
            else
            {
                ModelState.AddModelError("Passport", "Паспорт передан с ошибкой");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Passport.Add(passport);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = passport.ID }, passport);
        }

        // DELETE: api/Passports/5
        [ResponseType(typeof(Passport))]
        public IHttpActionResult DeletePassport(int id)
        {
            Passport passport = db.Passport.Find(id);
            if (passport == null)
            {
                return NotFound();
            }

            db.Passport.Remove(passport);
            db.SaveChanges();

            return Ok(passport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassportExists(int id)
        {
            return db.Passport.Count(e => e.ID == id) > 0;
        }
    }
}