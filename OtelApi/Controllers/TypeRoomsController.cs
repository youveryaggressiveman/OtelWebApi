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
    public class TypeRoomsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/TypeRooms
        public IQueryable<TypeRoom> GetTypeRoom()
        {
            return db.TypeRoom;
        }

        // GET: api/TypeRooms/5
        [ResponseType(typeof(TypeRoom))]
        public IHttpActionResult GetTypeRoom(int id)
        {
            TypeRoom typeRoom = db.TypeRoom.Find(id);
            if (typeRoom == null)
            {
                return NotFound();
            }

            return Ok(typeRoom);
        }

        // PUT: api/TypeRooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTypeRoom(int id, TypeRoom typeRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeRoom.ID)
            {
                return BadRequest();
            }

            db.Entry(typeRoom).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeRoomExists(id))
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

        // POST: api/TypeRooms
        [ResponseType(typeof(TypeRoom))]
        public IHttpActionResult PostTypeRoom(TypeRoom typeRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeRoom.Add(typeRoom);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TypeRoomExists(typeRoom.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = typeRoom.ID }, typeRoom);
        }

        // DELETE: api/TypeRooms/5
        [ResponseType(typeof(TypeRoom))]
        public IHttpActionResult DeleteTypeRoom(int id)
        {
            TypeRoom typeRoom = db.TypeRoom.Find(id);
            if (typeRoom == null)
            {
                return NotFound();
            }

            db.TypeRoom.Remove(typeRoom);
            db.SaveChanges();

            return Ok(typeRoom);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeRoomExists(int id)
        {
            return db.TypeRoom.Count(e => e.ID == id) > 0;
        }
    }
}