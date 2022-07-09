using BookStore.Models;
using BookStore.Models.Data;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class OrderRepository
    {
        public BaseList<GetOrder> GetAll(int pageIndex, int pageSize, int userId)
        {
            GetOrder order = null;
            List<GetOrder> orders = new List<GetOrder>();
            List<GetSubOrder> subOrders = null;
            List<OrderMst> orderMsts = null;
            List<OrderDtl> orderDtls = null;
            BaseList<GetOrder> result = new BaseList<GetOrder>();
            using (UnitOfWork db = new UnitOfWork())
            {
                orderMsts = db.OrderMsts.Where(order => (order.UserId == userId)).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable().ToList();

                foreach (OrderMst orderMst in orderMsts)
                {
                    order = new GetOrder();
                    order.Id = orderMst.Id;
                    order.UserId = orderMst.UserId;
                    order.OrderDate = orderMst.OrderDate;
                    order.TotalPrice = orderMst.TotalPrice;
                    orderDtls = db.OrderDtls.AsQueryable().Where(o => o.OrderMstId == orderMst.Id).ToList();

                    subOrders = new List<GetSubOrder>();
                    foreach (OrderDtl orderDtl in orderDtls)
                    {
                        GetSubOrder getSubOrder = new GetSubOrder();
                        Book book = db.Books.Where(b=>b.Id == orderDtl.BookId).FirstOrDefault();

                        getSubOrder.Book = new BookModel(book);
                        getSubOrder.Quantity = orderDtl.Quantity;
                        getSubOrder.Price = orderDtl.Price;
                        getSubOrder.TotalPrice = orderDtl.TotalPrice;
                        subOrders.Add(getSubOrder);
                    }

                    order.subOrders = subOrders;
                    orders.Add(order);
                }
            }
            result.TotalRecords = orders.Count();
            result.Records = orders;
            return result;
        }

        public Order Add(Order order)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                OrderMst orderMst = new OrderMst();
                orderMst.UserId = order.UserId;
                orderMst.OrderDate = order.OrderDate;
                orderMst.TotalPrice = order.TotalPrice;

                db.OrderMsts.Add(orderMst);
                db.SaveChanges();

                OrderDtl orderDtl = null;
                Book book = null;
                foreach (SubOrder subOrder in order.subOrders)
                {
                    book = db.Books.Where(b=>b.Id == subOrder.BookId).FirstOrDefault();
                    orderDtl = new OrderDtl();
                    orderDtl.OrderMstId = orderMst.Id;
                    orderDtl.BookId = subOrder.BookId;
                    orderDtl.Quantity = subOrder.Quantity;
                    orderDtl.Price = subOrder.Price;
                    orderDtl.TotalPrice = subOrder.TotalPrice;
                    db.OrderDtls.Add(orderDtl);
                    db.SaveChanges();

                    book.Quantity = book.Quantity - orderDtl.Quantity;
                    db.Books.Update(book);
                    db.SaveChanges();
                }
                return order;
            }
        }        
    }
}
