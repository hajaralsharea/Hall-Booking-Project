using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            ContactUs = new HashSet<ContactU>();
            Payments = new HashSet<Payment>();
            Testimonials = new HashSet<Testimonial>();
            UsersLogins = new HashSet<UsersLogin>();
        }

        public decimal Id { get; set; }
     
        public string Fname { get; set; }
    
        public string Lname { get; set; }
        
        public string Email { get; set; }
      
        public decimal? Phonenumber { get; set; }
        
        public string UserName { get; set; }
        
        public string ImagePath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }


        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<ContactU> ContactUs { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
        public virtual ICollection<UsersLogin> UsersLogins { get; set; }
    }
}
