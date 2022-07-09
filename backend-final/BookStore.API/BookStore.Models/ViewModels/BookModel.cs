using BookStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace BookStore.Models.ViewModels
{
    public class BookModel
    {
        public BookModel()
        {

        }

        public BookModel(Book book)
        {
            this.Id = book.Id;
            this.Name = book.Name;
            this.Price = book.Price;
            this.Description = book.Description;
            this.Base64image = book.Base64image;
            this.CategoryId = book.Categoryid;
            this.PublisherId = book.PublisherId;
            this.Quantity = book.Quantity;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Base64image { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int Quantity { get; set; }

        public Book ToEntity()
        {
            return new Book
            {
                Quantity = this.Quantity,
                PublisherId = this.PublisherId,
                Categoryid = this.CategoryId,
                Base64image= this.Base64image,
                Price = this.Price,
                Description = this.Description,
                Name = this.Name,
                Id = this.Id
            };
        }
    }
}
