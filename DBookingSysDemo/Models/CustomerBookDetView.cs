using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBookingSysDemo.Models
{
    public class CustomerBookDetView
    {

        //public Customer customer { get; set; }
        //public BookingDetail bookingDetail { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }


        public string UserType { get; set; }

        public int BookingId { get; set; }

        public float Package { get; set; }
        public DateTime PickUp { get; set; }

        public DateTime BTime { get; set; }

        public string Status { get; set; }

        public float Price { get; set; }
        public string DExecutive { get; set; }


    }
}
