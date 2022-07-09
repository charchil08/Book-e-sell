using BookStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class GetOrderModel
    {
        public GetOrderModel()
        {

        }

        public GetOrderModel(GetOrder order)
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
        public List<GetSubOrder> subOrders { get; set; }
        public double TotalPrice { get; set; }

        public GetOrder ToEntity()
        {
            return new GetOrder
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
