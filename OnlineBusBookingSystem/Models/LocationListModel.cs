using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusBookingSystem.Models
{
    public class LocationListModel
    {
        public int LocationId { get; set; }
        public string Terminal { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}