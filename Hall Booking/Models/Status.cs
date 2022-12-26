using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hall_Booking.Models
{
    public partial class Status
    {
        public Status()
        {
           Bookings = new HashSet<Booking>();
           Testimonials = new HashSet<Testimonial>();
        }

        public decimal Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}


