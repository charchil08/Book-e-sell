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
    public class CartRepository
    {       
        public BaseList<GetCartModel> GetAll(int pageIndex, int pageSize, int  UserId)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var query = db.Carts.AsQueryable();
                BaseList<GetCartModel> result = new BaseList<GetCartModel>();
                List<GetCartModel> getCartModels = new List<GetCartModel>();
                
                if (pageSize != 0)
                {
                    query = query.Where(cart => (cart.UserId == UserId)).Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    foreach (Cart cart in query.ToList())
                    {
                        GetCartModel getCartModel = new GetCartModel();
                        getCartModel.Id = cart.Id;
                        getCartModel.UserId = cart.UserId;

                        Book book = db.Books.Where(b => b.Id == cart.BookId).FirstOrDefault();
                        BookModel bookModel = new BookModel(book);
                        getCartModel.Book = bookModel;
                        getCartModel.Quantity = cart.Quantity;
                        getCartModels.Add(getCartModel);
                    }
                }
                result.TotalRecords = getCartModels.Count();
                result.Records = getCartModels;
                return result;
            }
        }

        public Cart AddToCart(Cart cart)
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                var cartInDb = db.Carts.FirstOrDefault(c => c.UserId == cart.UserId && c.BookId == cart.BookId);

                if (cartInDb == null)
                {
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return cart;
                }
                else
                {
                    throw new Exception($"book {cart.BookId} already exists in the cart for user {cart.UserId}. Update the quantity of existing item in the cart.");
                }
            }
        }

        public Cart UpdateCart(Cart cart)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                if (cart.Id == 0)
                {
                    throw new Exception("Cart item Id cannot be zero. Either supply existing cart item Id or use Update methord.");
                }

                Cart cartInDb = null;
                cartInDb = db.Carts.FirstOrDefault(c => c.Id == cart.Id);
                if (cartInDb == null)
                    throw new Exception($"Cart Item not found with id {cart.Id}");

                if (db.Carts.Any(c => c.Id != cart.Id && c.BookId == cart.BookId && c.UserId == cart.UserId))
                    throw new Exception($"Book with id {cart.BookId} already added in cart. Update the quantity of added item in the cart.");

                if (cartInDb == null)
                {
                    throw new Exception($"Cart item not found with cart item id {cart.Id}");
                }
                else
                {
                    cartInDb.Quantity = cart.Quantity;
                    db.Carts.Update(cartInDb);
                    db.SaveChanges();
                    return cartInDb;
                }                
            }
        }

        public Cart DeleteItem(int cartId)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Cart cart = db.Carts.FirstOrDefault(c => c.Id == cartId);
                if (cart == null)
                    throw new Exception($"Cart item not found with Id {cartId}");

                db.Carts.Remove(cart);
                db.SaveChanges();
                return cart;
            }
        }
    }
}
