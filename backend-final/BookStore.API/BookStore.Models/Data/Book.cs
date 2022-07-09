using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace BookStore.Models.Data
{
    public partial class Book
    {
        public Book()
        {
            OrderDtls = new HashSet<OrderDtl>();
            Carts = new HashSet<Cart>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Base64image { get; set; }
        public int Categoryid { get; set; }
        public int PublisherId { get; set; }
        public int Quantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<OrderDtl> OrderDtls { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
