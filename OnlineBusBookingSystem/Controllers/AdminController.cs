using Microsoft.Ajax.Utilities;
using OnlineBusBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OnlineBusBookingSystem.Controllers
{
    [CustomAuthenticationFilter]
    public class AdminController : Controller
    {

        BusDBEntities db = new BusDBEntities();

        public ActionResult LocationList()
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                List<LocationList> bus = db.LocationLists.ToList();
                List<LocationListModel> list = new List<LocationListModel>();

                foreach (var item in bus)
                {

                    LocationListModel blm = new LocationListModel
                    {
                        LocationId = item.LocationId,
                        Terminal = item.Terminal,
                        City = item.City,
                        State = item.State,
                    };

                    list.Add(blm);
                }
                return View(list); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult CreateLocation()
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                return View(); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult CreateLocation(LocationListModel model)
        {
            try
            {
                // TODO: Add insert logic here
                LocationList bus = new LocationList();
                bus.Terminal = model.Terminal;
                bus.City = model.City;
                bus.State = model.State;
                db.LocationLists.Add(bus);
                db.SaveChanges();
                TempData["msg"] = "Location successfully created!";
                return RedirectToAction("LocationList");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult EditLocation(int id)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                LocationList bus = db.LocationLists.Find(id);
                LocationListModel model = new LocationListModel();
                model.Terminal = bus.Terminal;
                model.City = bus.City;
                model.State = bus.State;
                return View(model); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult EditLocation(int id, LocationListModel model)
        {
            try
            {
                // TODO: Add update logic here
                LocationList bus = db.LocationLists.Find(id);
                bus.Terminal = model.Terminal;
                bus.City = model.City;
                bus.State = model.State;
                db.SaveChanges();
                return RedirectToAction("LocationList");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteLocation(int id)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                LocationList bus = db.LocationLists.Find(id);
                LocationListModel model = new LocationListModel();
                model.Terminal = bus.Terminal;
                model.City = bus.City;
                model.State = bus.State;
                return View(model); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult DeleteLocation(int id, LocationListModel model)
        {
            try
            {
                // TODO: Add delete logic here
                LocationList bus = db.LocationLists.Find(id);
                db.LocationLists.Remove(bus);
                db.SaveChanges();
                return RedirectToAction("LocationList");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult BookedList()
        {
            List<BookedList> bus = new List<BookedList>();
            if (Session["Role"] != null && Session["Role"].ToString() == "admin")
                bus = db.BookedLists.ToList();
            else if (db.BookedLists.Count() > 0)
            {
                var userId = Convert.ToInt32(Session["Id"]);
                bus = db.BookedLists.Where(r => r.UserId == userId).Select(r=>r).ToList();
            }
            List<BookedListModel> list = new List<BookedListModel>();

            foreach (var item in bus)
            {

                BookedListModel blm = new BookedListModel
                {
                    BusId = item.BusId,
                    ReferenceNo = item.ReferenceNo,
                    UserId = item.UserId,
                    ScheduleId = item.ScheduleId,
                    Name = item.Name,
                    Qty = item.Qty,
                    Amount = item.Amount,
                    Status = item.Status,
                    IsCancelled = item.IsCancelled
                };

                list.Add(blm);
            }
            TempData["Cancelled"] = "Booking Status: Cancelled";
            return View(list);
        }
        public ActionResult CreateBookedList()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateBookedList(BookedListModel model)
        {
            try
            {
                // TODO: Add insert logic here
                BookedList bus = new BookedList();
                bus.BusId = model.BusId;
                bus.UserId = model.UserId;
                bus.ScheduleId = model.ScheduleId;
                bus.Name = model.Name;
                bus.Qty = model.Qty;
                bus.Amount = model.Amount;
                bus.Status = model.Status;
                db.BookedLists.Add(bus);
                db.SaveChanges();
                TempData["msg"] = "Bus successfully booked!";
                return RedirectToAction("BookedList");
            }
            catch
            {
                return View();
            }
        }       
        public ActionResult BusList()
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                List<BusList> bus = db.BusLists.ToList();
                List<BusListModel> list = new List<BusListModel>();

                foreach (var item in bus)
                {

                    BusListModel blm = new BusListModel
                    {
                        BusNo = item.BusNo,
                        BusName = item.BusName,
                    };

                    list.Add(blm);
                }
                return View(list); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult CreateBusList()
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                return View(); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult CreateBusList(BusListModel model)
        {
            try
            {
                if (db.BusLists.Any(u => u.BusNo == model.BusNo))
                {
                    ModelState.AddModelError("BusNo", "This BusNo is already taken. Please choose a different one.");
                    return View(model);
                }
                // TODO: Add insert logic here
                BusList bus = new BusList();
                bus.BusNo = model.BusNo;
                bus.BusName = model.BusName;
                db.BusLists.Add(bus);
                db.SaveChanges();
                TempData["msg"] = "Bus successfully created!";
                return RedirectToAction("BusList");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult EditBusList(int id)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                BusList bus = db.BusLists.Find(id);
                BusListModel model = new BusListModel();
                model.BusNo = bus.BusNo;
                model.BusName = bus.BusName;
                return View(model); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult EditBusList(int id, BusListModel model)
        {
            try
            {
                // TODO: Add update logic here
                BusList bus = db.BusLists.Find(id);
                bus.BusNo = model.BusNo;
                bus.BusName = model.BusName;
                db.SaveChanges();
                return RedirectToAction("BusList");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteBusList(int id)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                BusList bus = db.BusLists.Find(id);
                BusListModel model = new BusListModel();
                model.BusNo = bus.BusNo;
                model.BusName = bus.BusName;
                return View(model); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult DeleteBusList(int id, BusListModel model)
        {
            try
            {
                // TODO: Add delete logic here
                BusList bus = db.BusLists.Find(id);
                db.BusLists.Remove(bus);
                db.SaveChanges();
                return RedirectToAction("BusList");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ScheduleList()
        {
            List<Schedule> bus = db.Schedules.ToList();
            List<ScheduleModel> list = new List<ScheduleModel>();

            foreach (var item in bus)
            {
                if(Session["Role"] != null && Session["Role"].ToString() == "admin")
                {
                    ScheduleModel blm = new ScheduleModel
                    {
                        BusId = item.BusId,
                        ScheduleId = item.ScheduleId,
                        BusName = db.BusLists.Where(r => r.BusNo == item.BusId).Select(r => r.BusNo + " | " + r.BusName).FirstOrDefault(),
                        FromLocationId = item.FromLocationId,
                        ToLocationId = item.ToLocationId,
                        Departure = item.Departure,
                        Arrival = item.Arrival,
                        Availability = item.Availability,
                        Price = item.Price,
                        Location = db.LocationLists.Where(r => r.LocationId == item.FromLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault() + " - " + db.LocationLists.Where(r => r.LocationId == item.ToLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault(),
                    };
                    var seatQty = db.BookedLists.Where(r => r.ScheduleId == item.ScheduleId && !r.IsCancelled).Sum(r => (int?)r.Qty) ?? 0;
                    blm.Availability = item.Availability - seatQty;
                    list.Add(blm);
                }
                if (item.Departure.AddMinutes(-30) >= DateTime.Now && Session["Role"] != null && Session["Role"].ToString() != "admin")
                {

                    ScheduleModel blm = new ScheduleModel
                    {
                        BusId = item.BusId,
                        ScheduleId = item.ScheduleId,
                        BusName = db.BusLists.Where(r => r.BusNo == item.BusId).Select(r => r.BusNo + " | " + r.BusName).FirstOrDefault(),
                        FromLocationId = item.FromLocationId,
                        ToLocationId = item.ToLocationId,
                        Departure = item.Departure,
                        Arrival = item.Arrival,
                        Availability = item.Availability,
                        Price = item.Price,
                        Location = db.LocationLists.Where(r => r.LocationId == item.FromLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault() + " - " + db.LocationLists.Where(r => r.LocationId == item.ToLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault(),
                    };
                    var seatQty = db.BookedLists.Where(r => r.ScheduleId == item.ScheduleId && !r.IsCancelled).Sum(r => (int?)r.Qty) ?? 0;
                    blm.Availability = item.Availability - seatQty;

                    list.Add(blm);
                }
            }
            return View(list);
        }


        public ActionResult CreateScheduleList()
        {
            ScheduleModel model = new ScheduleModel();
            model.AvailableLocation = db.LocationLists.Select(r => new SelectListItem()
            {
                Text = r.Terminal + "," + r.City + "," + r.State,
                Value = r.LocationId.ToString()
            }).ToList();
            model.AvailableBus = db.BusLists.Select(r => new SelectListItem()
            {
                Text = r.BusNo + " | " + r.BusName,
                Value = r.BusNo.ToString()
            }).ToList();
            return View(model);
        }


        [HttpPost]
        public ActionResult CreateScheduleList(ScheduleModel model)
        {
            try
            {
                model.AvailableLocation = db.LocationLists.Select(r => new SelectListItem()
                {
                    Text = r.Terminal + "," + r.City + "," + r.State,
                    Value = r.LocationId.ToString()
                }).ToList();
                model.AvailableBus = db.BusLists.Select(r => new SelectListItem()
                {
                    Text = r.BusNo + " | " + r.BusName,
                    Value = r.BusNo.ToString()
                }).ToList();
                if (IsDateValid(model.Departure, model.Arrival) && IsLocationDifferent(model.FromLocationId, model.ToLocationId) && IsDepartureGreaterThanCurrentDateTime(model.Departure))
                {
                    Schedule bus = new Schedule
                    {
                        BusId = model.BusId,
                        ScheduleId = model.ScheduleId,
                        BusName = db.BusLists.Where(r => r.BusNo == model.BusId).Select(r => r.BusName).FirstOrDefault(),
                        FromLocationId = model.FromLocationId,
                        ToLocationId = model.ToLocationId,
                        Departure = model.Departure,
                        Arrival = model.Arrival,
                        Availability = model.Availability,
                        Price = model.Price
                    };

                    db.Schedules.Add(bus);
                    db.SaveChanges();
                    TempData["msg"] = "Schedule successfully created!";
                    return RedirectToAction("ScheduleList");
                }
                else
                {
                    if (!IsDepartureGreaterThanCurrentDateTime(model.Departure))
                    {
                        ModelState.AddModelError("", "Departure date and time must be greater than current date and time.");
                    }
                    if (!IsDateValid(model.Departure, model.Arrival))
                    {
                        ModelState.AddModelError("", "Departure date and time must be lesser than arrival date and time.");
                    }

                    if (!IsLocationDifferent(model.FromLocationId, model.ToLocationId))
                    {
                        ModelState.AddModelError("", "From Location and To Location must be different.");
                    }

                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }
        private bool IsDepartureGreaterThanCurrentDateTime(DateTime departure)
        {
            return (departure > DateTime.Now);
        }
        private bool IsDateValid(DateTime departure, DateTime arrival)
        {
            return (departure < arrival);
        }
        private bool IsLocationDifferent(int fromLocationId, int toLocationId)
        {
            return fromLocationId != toLocationId;
        }

        public ActionResult BookDetails(int id)
        {
            ScheduleModel model = new ScheduleModel();
            Schedule bus = db.Schedules.Find(id);
            model.BusId = bus.BusId;
            model.ScheduleId = bus.ScheduleId;
            model.BusName = bus.BusId + " | " + bus.BusName;
            model.FromLocationId = bus.FromLocationId;
            model.ToLocationId = bus.ToLocationId;
            model.FromLocation = db.LocationLists.Where(r => r.LocationId == model.FromLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault();
            model.ToLocation = db.LocationLists.Where(r => r.LocationId == model.ToLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault();
            model.Departure = bus.Departure;
            model.Arrival = bus.Arrival;
            model.Price = bus.Price;
            var seatQty = db.BookedLists.Where(r => r.ScheduleId == model.ScheduleId && !r.IsCancelled).Sum(r => (int?)r.Qty) ?? 0;
            model.Availability = bus.Availability - seatQty;
            return View(model);
        }
        [HttpPost]
        public ActionResult BookDetails(ScheduleModel model)
        {
            if (model.Seat <= 0)
            {
                ModelState.AddModelError("Seat", "Please input seat qty");
                return View(model);
            }
            if (model.Availability < model.Seat)
            {
                ModelState.AddModelError("Seat", "Seat not available!");
                return View(model);
            }
            BookedList bus = new BookedList();
            bus.BusId = model.BusId;
            bus.ScheduleId = model.ScheduleId;
            bus.Qty = model.Seat;
            bus.Amount = (model.Seat * model.Price);
            bus.UserId = Convert.ToInt32(Session["Id"]);
            bus.Name = Session["Name"].ToString();
            bus.Status = Status.Unpaid.ToString();
            db.BookedLists.Add(bus);
            db.SaveChanges();
            return RedirectToAction("BookedList");
        }
        public ActionResult EditScheduleList(int id)
        {
            ScheduleModel model = new ScheduleModel();
            model.AvailableLocation = db.LocationLists.Select(r => new SelectListItem()
            {
                Text = r.Terminal + "," + r.City + "," + r.State,
                Value = r.LocationId.ToString()
            }).ToList();
            model.AvailableBus = db.BusLists.Select(r => new SelectListItem()
            {
                Text = r.BusNo + " | " + r.BusName,
                Value = r.BusNo.ToString()
            }).ToList();
            Schedule bus = db.Schedules.Find(id);
            //ScheduleModel model = new ScheduleModel();
            model.BusId = bus.BusId;
            model.ScheduleId = bus.ScheduleId;
            model.BusName = bus.BusName;
            model.FromLocationId = bus.FromLocationId;
            model.ToLocationId = bus.ToLocationId;
            model.Departure = bus.Departure;
            model.Arrival = bus.Arrival;
            model.Availability = bus.Availability;
            model.Price = bus.Price;
            return View(model);
        }


        [HttpPost]
        public ActionResult EditScheduleList(int id, ScheduleModel model)
        {
            try
            {
                model.AvailableLocation = db.LocationLists.Select(r => new SelectListItem()
                {
                    Text = r.Terminal + "," + r.City + "," + r.State,
                    Value = r.LocationId.ToString()
                }).ToList();
                model.AvailableBus = db.BusLists.Select(r => new SelectListItem()
                {
                    Text = r.BusNo + " | " + r.BusName,
                    Value = r.BusNo.ToString()
                }).ToList();
                if (IsDateValid(model.Departure, model.Arrival) && IsLocationDifferent(model.FromLocationId, model.ToLocationId) && IsDepartureGreaterThanCurrentDateTime(model.Departure))
                {
                    Schedule bus = db.Schedules.Find(id);
                    bus.BusId = model.BusId;
                    bus.BusName = db.BusLists.Where(r => r.BusNo == model.BusId).Select(r => r.BusName).FirstOrDefault();
                    bus.FromLocationId = model.FromLocationId;
                    bus.ToLocationId = model.ToLocationId;
                    bus.Departure = model.Departure;
                    bus.Arrival = model.Arrival;
                    bus.Availability = model.Availability;
                    bus.Price = model.Price;
                    db.SaveChanges();
                    return RedirectToAction("ScheduleList");
                }
                else
                {
                    if (!IsDepartureGreaterThanCurrentDateTime(model.Departure))
                    {
                        ModelState.AddModelError("", "Departure date and time must be greater than current date and time.");
                    }
                    if (!IsDateValid(model.Departure, model.Arrival))
                    {
                        ModelState.AddModelError("", "Departure date and time must be lesser than arrival date and time.");
                    }

                    if (!IsLocationDifferent(model.FromLocationId, model.ToLocationId))
                    {
                        ModelState.AddModelError("", "From Location and To Location must be different.");
                    }

                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteScheduleList(int id)
        {
            Schedule bus = db.Schedules.Find(id);
            ScheduleModel model = new ScheduleModel();
            model.BusId = bus.BusId;
            model.BusName = db.BusLists.Where(r => r.BusNo == model.BusId).Select(r => r.BusName).FirstOrDefault();
            model.FromLocationId = bus.FromLocationId;
            model.ToLocationId = bus.ToLocationId;
            model.Departure = bus.Departure;
            model.Arrival = bus.Arrival;
            model.Availability = bus.Availability;
            model.Price = bus.Price;
            return View(model);
        }


        [HttpPost]
        public ActionResult DeleteScheduleList(int id, ScheduleModel model)
        {
            try
            {
                // TODO: Add delete logic here
                Schedule bus = db.Schedules.Find(id);
                db.Schedules.Remove(bus);
                db.SaveChanges();
                return RedirectToAction("ScheduleList");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditBookedList(int id)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "admin"))
            {
                var bookedList = db.BookedLists.Find(id);
                Schedule bus = db.Schedules.Find(bookedList.ScheduleId);
                BookedListModel model = new BookedListModel();
                model.BusId = bus.BusId;
                var busName = db.BusLists.Where(r => r.BusNo == model.BusId).Select(r => r.BusName).FirstOrDefault();
                model.BusName = bus.BusId + " | " + busName;
                model.Departure = bus.Departure;
                model.Arrival = bus.Arrival;
                model.Name = bookedList.Name;
                model.Status = bookedList.Status;
                model.FromLocation = db.LocationLists.Where(r => r.LocationId == bus.FromLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault();
                model.ToLocation = db.LocationLists.Where(r => r.LocationId == bus.ToLocationId).Select(r => r.Terminal + "," + r.City + "," + r.State).FirstOrDefault();
                model.Qty = bookedList.Qty;
                model.ReferenceNo = id;
                IEnumerable<Status> values = Enum.GetValues(typeof(Status))
                                            .Cast<Status>();
                IList<SelectListItem> items = (from value in values
                                               select new SelectListItem()
                                               {
                                                   Text = value.ToString(),
                                                   Value = value.ToString(),
                                               }).ToList();
                model.AvailablePaymentStatus = items;
                return View(model); 
            }
            else
            {
                TempData["Error"] = "You are not authorized to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditBookedList(BookedListModel model)
        {
            try
            {
                // TODO: Add update logic here
                BookedList bus = db.BookedLists.Find(model.ReferenceNo);
                bus.Status = model.Status;
                db.SaveChanges();
                return RedirectToAction("BookedList");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CancelBook(int ReferenceNo)
        {
            BookedList bus = db.BookedLists.Find(ReferenceNo);
            bus.IsCancelled = true;
            db.SaveChanges();
            TempData["msg"] = "Booking cancelled successfully!";
            return RedirectToAction("BookedList","Admin");
        }
    }
}