using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace BookStore.Models.Data
{
    public partial class OrderMst
    {
        public OrderMst()
        {
            OrderDtls = new HashSet<OrderDtl>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDtl> OrderDtls {get;set;}
    }
}
