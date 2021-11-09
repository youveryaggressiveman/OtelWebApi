﻿using System;
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
    public class RoomsController : ApiController
    {
        private OtelEntities db = new OtelEntities();

        // GET: api/Rooms
        public IQueryable<Room> GetRoom()
        {
            return db.Room;
        }

        [Route("api/Rooms/typeRoomsId")]
        [ResponseType(typeof(Room))]
        public IQueryable<Room> GetRoomByTypeRoom(int id)
        {
            var result = (from c in db.Room
                          join o in db.TypeRoom on c.TypeRoomID equals o.ID
                          where o.ID == id
                          select c).Distinct();
            return result;
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            Room room = db.Room.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.ID)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Room.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.ID }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Room.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Room.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Room.Count(e => e.ID == id) > 0;
        }
    }
}