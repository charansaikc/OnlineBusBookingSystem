using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBusBookingSystem.Models
{
    public enum Status
    {
        Paid,
        Unpaid
    }
    public class ScheduleModel
    {
        
        public ScheduleModel()
        {
            AvailableLocation = new List<SelectListItem>();
            AvailableBus = new List<SelectListItem>();
        }
        [Required]
        public int BusId { get; set; }
        public int ScheduleId { get; set; }
        [DisplayName("Bus Name")]
        public string BusName { get; set; }
        [DisplayName("Departure Time")]
        [DataType(DataType.Date)]
        [Required]
        public System.DateTime Departure { get; set; }
        [DisplayName("Estimated Arrival Time")]
        [DataType(DataType.Date)]
        [Required]
        public System.DateTime Arrival { get; set; }
        [Required]
        public int Availability { get; set; }
        [Required]
        public int Price { get; set; }
        [DisplayName("From")]
        [Required]
        public int FromLocationId { get; set; }
        [Required]
        [DisplayName("To")]
        public int ToLocationId { get; set; }
        public int Seat { get; set; }
        public string Location { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public IList<SelectListItem> AvailableLocation { get; set; }
        public IList<SelectListItem> AvailableBus { get; set; }
    }
}