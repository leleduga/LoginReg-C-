using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginReg.Models
{
    public class RestViewModel
    {
        [Display(Name = "First Name:")]
        [Required]
        [MinLength (2, ErrorMessage="Must have more than 2 characters!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required]
        [MinLength (2, ErrorMessage="Must have more than 2 characters!")]

        public string LastName { get; set; }

        [Display(Name = "Email:")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password:")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength (8, ErrorMessage="Must have more than 8 characters!")]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword:")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}