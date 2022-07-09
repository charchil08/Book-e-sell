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
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        [Route("list")]
        public BaseList<GetBookModel> GetBooks(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            BookRepository repo = new BookRepository();
            BaseList<GetBookModel> books = repo.GetAll(pageIndex, pageSize, keyword);
            return books;
        }

        [HttpGet]
        [Route("{id}")]
        public GetBookModel GetBook([FromRoute] int id)
        {
            BookRepository repo = new BookRepository();
            return repo.GetBookModelById(id);
        }

        [HttpPost]
        [Route("Add")]
        public BookModel AddBook(BookModel book)
        {
            BookRepository repo = new BookRepository();
            return new BookModel(repo.Add(book.ToEntity()));
        }

        [HttpPut]
        [Route("Update")]
        public BookModel UpdateBook(BookModel book)
        {
            BookRepository repo = new BookRepository();
            return new BookModel(repo.Update(book.ToEntity()));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public BookModel DeleteBook([FromRoute] int id)
        {
            BookRepository repo = new BookRepository();
            return new BookModel(repo.Delete(id));
        }
    }
}
