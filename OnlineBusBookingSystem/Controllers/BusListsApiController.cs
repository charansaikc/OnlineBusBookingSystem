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
using OnlineBusBookingSystem;

namespace OnlineBusBookingSystem.Controllers
{
    public class BusListsApiController : ApiController
    {
        private BusDBEntities db = new BusDBEntities();

        // GET: api/BusListsApi
        public IQueryable<BusList> GetBusLists()
        {
            return db.BusLists;
        }

        // GET: api/BusListsApi/5
        [ResponseType(typeof(BusList))]
        public IHttpActionResult GetBusList(int id)
        {
            BusList busList = db.BusLists.Find(id);
            if (busList == null)
            {
                return NotFound();
            }

            return Ok(busList);
        }

        // PUT: api/BusListsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBusList(int id, BusList busList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != busList.BusNo)
            {
                return BadRequest();
            }

            db.Entry(busList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusListExists(id))
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

        // POST: api/BusListsApi
        [ResponseType(typeof(BusList))]
        public IHttpActionResult PostBusList(BusList busList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BusLists.Add(busList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BusListExists(busList.BusNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = busList.BusNo }, busList);
        }

        // DELETE: api/BusListsApi/5
        [ResponseType(typeof(BusList))]
        public IHttpActionResult DeleteBusList(int id)
        {
            BusList busList = db.BusLists.Find(id);
            if (busList == null)
            {
                return NotFound();
            }

            db.BusLists.Remove(busList);
            db.SaveChanges();

            return Ok(busList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusListExists(int id)
        {
            return db.BusLists.Count(e => e.BusNo == id) > 0;
        }
    }
}