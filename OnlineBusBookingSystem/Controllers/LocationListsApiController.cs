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
    public class LocationListsApiController : ApiController
    {
        private BusDBEntities db = new BusDBEntities();

        // GET: api/LocationListsApi
        public IQueryable<LocationList> GetLocationLists()
        {
            return db.LocationLists;
        }

        // GET: api/LocationListsApi/5
        [ResponseType(typeof(LocationList))]
        public IHttpActionResult GetLocationList(int id)
        {
            LocationList locationList = db.LocationLists.Find(id);
            if (locationList == null)
            {
                return NotFound();
            }

            return Ok(locationList);
        }

        // PUT: api/LocationListsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocationList(int id, LocationList locationList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationList.LocationId)
            {
                return BadRequest();
            }

            db.Entry(locationList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationListExists(id))
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

        // POST: api/LocationListsApi
        [ResponseType(typeof(LocationList))]
        public IHttpActionResult PostLocationList(LocationList locationList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LocationLists.Add(locationList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = locationList.LocationId }, locationList);
        }

        // DELETE: api/LocationListsApi/5
        [ResponseType(typeof(LocationList))]
        public IHttpActionResult DeleteLocationList(int id)
        {
            LocationList locationList = db.LocationLists.Find(id);
            if (locationList == null)
            {
                return NotFound();
            }

            db.LocationLists.Remove(locationList);
            db.SaveChanges();

            return Ok(locationList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationListExists(int id)
        {
            return db.LocationLists.Count(e => e.LocationId == id) > 0;
        }
    }
}