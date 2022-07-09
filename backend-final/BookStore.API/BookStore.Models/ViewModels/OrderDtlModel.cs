using BookStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class OrderDtlModel
    {
        public OrderDtlModel()
        {

        }

        public OrderDtlModel(OrderDtl orderDtl)
        {
            this.Id = orderDtl.Id;
            this.OrderMstId = orderDtl.OrderMstId;
            this.BookId = orderDtl.BookId;
            this.Quantity = orderDtl.Quantity;
            this.Price = orderDtl.Price;
            this.TotalPrice = orderDtl.TotalPrice;
        }
        public int Id { get; set; }
        public int OrderMstId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }

        public SubOrder ToEntity()
        {
            return new SubOrder
            {
                BookId = this.BookId,
                Quantity = this.Quantity,
                Price = this.Price,
                TotalPrice = this.TotalPrice
            };
        }
    }
}
