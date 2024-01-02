using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBusBookingSystem.Models
{
    public class BookedListModel
    {
        public BookedListModel() { AvailablePaymentStatus = new List<SelectListItem>(); }    
        public int BusId { get; set; }
        public int ReferenceNo { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public int ScheduleId { get; set; }
        public string BusName { get; set; }
        public System.DateTime Departure { get; set; }
        public System.DateTime Arrival { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public bool IsCancelled { get; set; }
        public IList<SelectListItem> AvailablePaymentStatus { get; set; }
    }
}