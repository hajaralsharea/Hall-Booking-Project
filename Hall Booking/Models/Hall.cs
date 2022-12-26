using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class Hall
    {
        public Hall()
        {
            Bookings = new HashSet<Booking>();
        }

        public decimal Id { get; set; }
        public string HallName { get; set; }
        public string HallAddress { get; set; }
        public decimal? Capacity { get; set; }
        public decimal? Price { get; set; }
        public string HallArea { get; set; }
        public decimal? CategoryId { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        public virtual HallCategory HallCategory { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
