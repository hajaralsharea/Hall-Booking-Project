using System;
using System.Collections.Generic;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public string Commint { get; set; }
        public decimal? UserId { get; set; }
        public decimal? StatusId { get; set; }
        public virtual User User { get; set; }
        public virtual Status Status { get; set; }


    }
}
