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
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("list")]
        public BaseList<CategoryModel> GetCategories(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            CategoryRepository repo = new CategoryRepository();
            BaseList<Category> categories = repo.GetAll(pageIndex, pageSize, keyword);
            return new BaseList<CategoryModel> { TotalRecords = categories.TotalRecords, Records = categories.Records.Select(record => new CategoryModel(record)).ToList() };
        }

        [HttpGet]
        [Route("{id}")]
        public CategoryModel GetCategory([FromRoute]int id)
        {
            CategoryRepository repo = new CategoryRepository();
            return new CategoryModel(repo.GetById(id));
        }

        [HttpPost]
        [Route("Add")]
        public CategoryModel AddCategory(CategoryModel category)
        {
            CategoryRepository repo = new CategoryRepository();
            return new CategoryModel(repo.Add(category.ToEntity()));
        }

        [HttpPut]
        [Route("Update")]
        public CategoryModel UpdateCategory(CategoryModel category)
        {
            CategoryRepository repo = new CategoryRepository();
            return new CategoryModel(repo.Update(category.ToEntity()));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CategoryModel DeleteCategory([FromRoute]int id)
        {
            CategoryRepository repo = new CategoryRepository();
            return new CategoryModel(repo.Delete(id));
        }
    }
}
