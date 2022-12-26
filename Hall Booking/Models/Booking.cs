using System;
using System.Collections.Generic;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class Booking
    {
        public Booking()
        {
            MailRequests = new HashSet<MailRequest>();
        }

        public decimal Id { get; set; }
        public string BookingNotes { get; set; }
        public DateTime? BookingDate { get; set; }
        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? HallId { get; set; }
        public decimal? UserId { get; set; }
        public decimal? StatusId { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual User User { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<MailRequest> MailRequests { get; set; }
    }
}
