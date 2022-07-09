using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models.Data
{
    public partial class Cart
    {
        public Cart()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
