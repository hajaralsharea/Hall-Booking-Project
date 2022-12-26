using System;
using System.Collections.Generic;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class Payment
    {
        public decimal Id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string CardName { get; set; }
        public decimal? CardSequanceNumber { get; set; }
        public decimal? CardBalance { get; set; }
        public decimal? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
