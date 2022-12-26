using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class UsersLogin
    {
        public decimal Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Passwordd { get; set; }
        
        public decimal? RoleId { get; set; }
        public decimal? UserId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
