using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBookingSysDemo.Models
{
    public class BookingStatus
    {

        public int BookingStatusId { get; set; }
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public DateTime PickUp { get; set; }

        public float Package { get; set; }

        public DateTime BTime { get; set; }

        public string Status { get; set; }

        public int Price { get; set; }

        public string DExecutive { get; set; }

        public string FromCity { get; set; }

        public string ToCity { get; set; }

        public string Reason { get; set; }

    }
}
