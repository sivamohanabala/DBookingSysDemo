using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBookingSysDemo.Models
{
    public class Customer
    {
       
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Minimum Five Character should be there ")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are not allowed.")]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string CPassword { get; set; }

        public int Age { get; set; }

        [StringLength(10, ErrorMessage = "Phone number should be length of 10")]
        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "User Type Required")]

        public string UserType { get; set; }

        public bool IsVerified { get; set; }

       public List<BookingDetail> BookingDetails { get; set; }
    }
}
