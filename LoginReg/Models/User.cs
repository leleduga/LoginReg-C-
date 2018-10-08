using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginReg.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // public System.DateTime created_at { get; set; } = System.DateTime.Now;
        // public System.DateTime updated_at {get; set; } = System.DateTime.Now;
    }
}