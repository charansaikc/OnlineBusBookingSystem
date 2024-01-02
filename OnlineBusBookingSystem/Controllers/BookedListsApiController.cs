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
    public class BookedListsApiController : ApiController
    {
        private BusDBEntities db = new BusDBEntities();

        // GET: api/BookedListsApi
        public IQueryable<BookedList> GetBookedLists()
        {
            return db.BookedLists;
        }

        // GET: api/BookedListsApi/5
        [ResponseType(typeof(BookedList))]
        public IHttpActionResult GetBookedList(int id)
        {
            BookedList bookedList = db.BookedLists.Find(id);
            if (bookedList == null)
            {
                return NotFound();
            }

            return Ok(bookedList);
        }

        // PUT: api/BookedListsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookedList(int id, BookedList bookedList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookedList.ReferenceNo)
            {
                return BadRequest();
            }

            db.Entry(bookedList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookedListExists(id))
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

        // POST: api/BookedListsApi
        [ResponseType(typeof(BookedList))]
        public IHttpActionResult PostBookedList(BookedList bookedList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookedLists.Add(bookedList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bookedList.ReferenceNo }, bookedList);
        }

        // DELETE: api/BookedListsApi/5
        [ResponseType(typeof(BookedList))]
        public IHttpActionResult DeleteBookedList(int id)
        {
            BookedList bookedList = db.BookedLists.Find(id);
            if (bookedList == null)
            {
                return NotFound();
            }

            db.BookedLists.Remove(bookedList);
            db.SaveChanges();

            return Ok(bookedList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookedListExists(int id)
        {
            return db.BookedLists.Count(e => e.ReferenceNo == id) > 0;
        }
    }
}