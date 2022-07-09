using BookStore.Models;
using BookStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class CategoryRepository
    {
        public BaseList<Category> GetAll(int pageIndex, int pageSize, string keyword)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var query = db.Categories.AsQueryable();
                BaseList<Category> result = new BaseList<Category>();
                result.TotalRecords = query.Count();
                if (pageSize != 0)
                {
                    if (pageIndex != 0)
                    {
                        query = query.Where(category => (keyword == default || category.Name.ToLower().Contains(keyword.ToLower()))).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                        if (keyword != default)
                        {
                            result.TotalRecords = query.Count();
                        }
                    }
                }

                result.Records = query.ToList();
                return result;
            }
        }

        public Category GetById(int id)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Category category = db.Categories.Where(x => x.Id == id).FirstOrDefault();

                if (category == null)
                {
                    throw new Exception(Messages.InvalidCredentialsMessage);
                }
                else
                {
                    return category;
                }
            }
        }

        public Category Add(Category cat)
        {

            using (UnitOfWork db = new UnitOfWork())
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return cat;
            }
        }

        public Category Update(Category cat)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                db.Categories.Update(cat);
                db.SaveChanges();
                return cat;
            }
        }

        public Category Delete(int categoryId)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Category cat = db.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (cat == null)
                    throw new Exception($"Category not found with Id {categoryId}");

                db.Categories.Remove(cat);
                db.SaveChanges();
                return cat;
            }
        }
    }
}
