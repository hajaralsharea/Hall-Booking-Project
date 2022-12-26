using System;
using System.Collections.Generic;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class ContactU
    {
        public decimal Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public decimal? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
