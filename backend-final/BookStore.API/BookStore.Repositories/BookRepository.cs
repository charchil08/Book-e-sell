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
    public class BookRepository
    {
        public BaseList<GetBookModel> GetAll(int pageIndex, int pageSize, string keyword)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var query = db.Books.AsQueryable();
                BaseList<GetBookModel> result = new BaseList<GetBookModel>();
                List<GetBookModel> getBookModels = new List<GetBookModel>();

                if (pageSize != 0)
                {
                    query = query.Where(category => (keyword == default || category.Name.ToLower().Contains(keyword.ToLower()))).Skip((pageIndex - 1) * pageSize).Take(pageSize);                   
                }

                GetBookModel getBookModel = null;
                foreach (Book book in query.ToList())
                {
                    getBookModel = new GetBookModel();
                    getBookModel.Id = book.Id;
                    getBookModel.Name = book.Name;
                    getBookModel.Price = book.Price;
                    getBookModel.Description = book.Description;
                    getBookModel.Base64image = book.Base64image;

                    Category category = db.Categories.Where(c => c.Id == book.Categoryid).FirstOrDefault();
                    getBookModel.Category = category.Name;

                    Publisher publisher = db.Publishers.Where(p => p.Id == book.PublisherId).FirstOrDefault();
                    getBookModel.Publisher = publisher.Name;
                    getBookModel.Quantity = book.Quantity;
                    getBookModels.Add(getBookModel);
                }

                result.TotalRecords = query.Count();
                result.Records = getBookModels;
                return result;
            }
        }

        public Book GetById(int id)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Book book = db.Books.Where(x => x.Id == id).FirstOrDefault();

                if (book == null)
                {
                    throw new Exception(Messages.InvalidCredentialsMessage);
                }
                else
                {
                    return book;
                }
            }
        }

        public GetBookModel GetBookModelById(int id)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Book book = db.Books.Where(x => x.Id == id).FirstOrDefault();

                if (book == null)
                {
                    throw new Exception(Messages.InvalidCredentialsMessage);
                }
                else
                {
                    GetBookModel bookModel = new GetBookModel();
                    bookModel.Id = book.Id;
                    bookModel.Name = book.Name;
                    bookModel.Price = book.Price;
                    bookModel.Description = book.Description;
                    bookModel.Base64image = book.Base64image;

                    Category category = db.Categories.Where(c => c.Id == book.Categoryid).FirstOrDefault();
                    bookModel.Category = category.Name;

                    Publisher publisher = db.Publishers.Where(p=>p.Id == book.PublisherId).FirstOrDefault();
                    bookModel.Publisher = publisher.Name;
                    bookModel.Quantity = book.Quantity;
                    return bookModel;
                }
            }
        }

        public Book Add(Book book)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                db.Books.Add(book);
                db.SaveChanges();
                return book;
            }
        }

        public Book Update(Book book)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                db.Books.Update(book);
                db.SaveChanges();
                return book;
            }
        }

        public Book Delete(int bookId)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Book book = db.Books.FirstOrDefault(c => c.Id == bookId);
                if (book == null)
                    throw new Exception($"Book not found with Id {bookId}");

                db.Books.Remove(book);
                db.SaveChanges();
                return book;
            }
        }
    }
}
