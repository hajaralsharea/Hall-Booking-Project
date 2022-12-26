using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class MailRequest
    {
        public decimal Id { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FilePath { get; set; }
        public decimal? BookingId { get; set; }
     

        public virtual Booking Booking { get; set; }
    }
}
