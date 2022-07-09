using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Data;

namespace BookStore.Models.ViewModels
{
    public class OrderModel
    {
        public OrderModel()
        {

        }

        public OrderModel(Order order)
        {
            this.Id = order.Id;
            this.UserId = order.UserId;
            this.OrderDate = order.OrderDate;
            this.subOrders = order.subOrders;
            this.TotalPrice = order.TotalPrice;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<SubOrder> subOrders { get; set; }
        public double TotalPrice { get; set; }

        public Order ToEntity()
        {
            return new Order
            {
                TotalPrice = this.TotalPrice,
                subOrders = this.subOrders,
                OrderDate = this.OrderDate,
                UserId = this.UserId,
                Id = this.Id
            };
        }
    }
}
