using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace BookStore.Models.Data
{
    public partial class OrderDtl
    {
        public int Id { get; set; }
        public int OrderMstId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }

        public virtual OrderMst OrderMst { get; set; }
        public virtual Book Book { get; set; }
    }
}
