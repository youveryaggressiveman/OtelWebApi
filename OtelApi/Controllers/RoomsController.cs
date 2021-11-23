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

        // GET: api/Rooms/order
        [Route("api/Rooms/order")]
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoomByOrderId(int id)
        {
            var room = from r in db.Room
                       join o in db.Order on r.ID equals o.ID
                       where o.ID == id
                       select r;

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // GET: api/Rooms/otel
        [Route("api/Rooms/otel")]
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoomByOtelId(int id, DateTime date, int typeRoom)
        {
            List<Room> number = new List<Room>();
            var ordres = db.Order;
            var roomByOtel = from r in db.Room
                             join o in db.Otel on r.OtelID equals o.ID
                             where o.ID == id
                             select r;

            var roomByTypeRoom = from d in db.Room
                                 join t in db.TypeRoom on d.TypeRoomID equals t.ID
                                 where t.ID == typeRoom
                                 select d;

            foreach (var item in roomByOtel)
            {
                foreach (var item2 in roomByTypeRoom)
                {

                    if (item == item2)
                    {
                        number.Add(item2);
                    }

                }

            }

            List<Room> result = number;

            foreach (var item in ordres)
            {
                foreach (var itemRoom in number)
                {
                    if (item.Room.Contains(itemRoom))
                    {
                        if (item.DepartureDate >= date)
                        {
                            result.Remove(itemRoom);
                        }
                    }
                }

            }

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
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