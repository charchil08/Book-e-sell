using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Data;

namespace BookStore.Models.ViewModels
{
    public class CartModel
    {
        public CartModel()
        {

        }

        public CartModel(Cart cart)
        {
            this.Id = cart.Id;
            this.UserId = cart.UserId;
            this.BookId = cart.BookId;
            this.Quantity = cart.Quantity;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public Cart ToEntity()
        {
            return new Cart
            {
                Id = this.Id,
                UserId = this.UserId,
                BookId = this.BookId,
                Quantity = this.Quantity
            };
        }
    }
}
