using BookStore.Models;
using BookStore.Models.Data;
using BookStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Models.ViewModels;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : Controller
    {
        [HttpGet]
        [Route("list")]
        public BaseList<GetOrderModel> GetOrders(int pageIndex = 1, int pageSize = 10, int userId = 0)
        {
            OrderRepository repo = new OrderRepository();
            BaseList<GetOrder> order = repo.GetAll(pageIndex, pageSize, userId);
            return new BaseList<GetOrderModel> { TotalRecords = order.TotalRecords, Records = order.Records.Select(record => new GetOrderModel(record)).ToList() };
        }

        [HttpPost]
        [Route("Add")]
        public OrderModel AddOrder(OrderModel order)
        {
            OrderRepository repo = new OrderRepository();
            return new OrderModel(repo.Add(order.ToEntity()));
        }
    }
}
