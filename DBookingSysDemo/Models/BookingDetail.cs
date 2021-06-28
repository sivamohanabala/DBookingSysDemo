using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBookingSysDemo.Models
{
    public class BookingDetail
    {
      // public Customer Customer { get; set; }
        public int BookingId { get; set; }

        public int UserId { get; set; }

     
        [Required]
       
        public DateTime PickUp { get; set; }

       
        public float Package { get; set; }

        [Required]
        public DateTime BTime { get; set; }

        public string Status { get; set; }

        public float Price { get; set; }

        public string DExecutive { get; set; }
    }
}
