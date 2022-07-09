using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models.Data
{
    public partial class User
    {
        public User()
        {
            OrderMsts = new HashSet<OrderMst>();
            Carts = new HashSet<Cart>();
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Roleid { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<OrderMst> OrderMsts { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
