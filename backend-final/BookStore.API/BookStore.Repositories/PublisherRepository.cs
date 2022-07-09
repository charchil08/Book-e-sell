using BookStore.Models;
using BookStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class PublisherRepository
    {
        public BaseList<Publisher> GetAll(int pageIndex, int pageSize, string keyword)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var query = db.Publishers.AsQueryable();
                BaseList<Publisher> result = new BaseList<Publisher>();
                if (pageSize != 0)
                {
                    query = query.Where(category => (keyword == default || category.Name.ToLower().Contains(keyword.ToLower()))).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    if (keyword != default)
                    {
                        result.TotalRecords = query.Count();
                    }
                }

                result.TotalRecords = query.Count();
                result.Records = query.ToList();
                return result;
            }
        }

        public Publisher GetById(int id)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Publisher publisher = db.Publishers.Where(x => x.Id == id).FirstOrDefault();

                if (publisher == null)
                {
                    throw new Exception(Messages.InvalidCredentialsMessage);
                }
                else
                {
                    return publisher;
                }
            }
        }

        public Publisher Add(Publisher publisher)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
                return publisher;
            }
        }

        public Publisher Update(Publisher publisher)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                db.Publishers.Update(publisher);
                db.SaveChanges();
                return publisher;
            }
        }

        public Publisher Delete(int publisherId)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                Publisher publisher = db.Publishers.FirstOrDefault(c => c.Id == publisherId);
                if (publisher == null)
                    throw new Exception($"Publusher not found with Id {publisherId}");

                db.Publishers.Remove(publisher);
                db.SaveChanges();
                return publisher;
            }
        }
    }
}
