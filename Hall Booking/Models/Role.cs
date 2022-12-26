using System;
using System.Collections.Generic;

#nullable disable

namespace Hall_Booking.Models
{
    public partial class Role
    {
        public Role()
        {
            UsersLogins = new HashSet<UsersLogin>();
        }

        public decimal Id { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<UsersLogin> UsersLogins { get; set; }
    }
}
