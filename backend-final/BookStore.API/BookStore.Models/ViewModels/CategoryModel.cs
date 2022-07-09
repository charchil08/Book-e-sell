using BookStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.ViewModels
{
    public class CategoryModel
    {
        public CategoryModel ()
        {

        }
        public CategoryModel(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }
            public int Id { get; set; }
            public string Name { get; set; }


        public Category ToEntity()
        {
            return new Category
            {
                Id = this.Id,
                Name= this.Name,
            };
        }
    }
}
