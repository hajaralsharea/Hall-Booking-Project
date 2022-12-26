using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Hall_Booking.Models
{
    public partial class HallCategory
    {
        public HallCategory()
        {
            Halls = new HashSet<Hall>();
        }

        public decimal Id { get; set; }
        public string CategoryName { get; set; }
        public string HallDescription { get; set; }

        public string ImagePath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }


        public virtual ICollection<Hall> Halls { get; set; }
    }
}

