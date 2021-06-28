using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DBookingSysDemo.Models;

namespace DBookingSysDemo.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserType { get; set; }

    }
}
