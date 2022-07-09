using BookStore.Models;
using BookStore.Models.Data;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/Publisher")]
    [ApiController]
    public class PublisherController : Controller
    {
        [HttpGet]
        [Route("list")]
        public BaseList<PublisherModel> GetPublishers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            PublisherRepository repo = new PublisherRepository();
            BaseList<Publisher> publisher = repo.GetAll(pageIndex, pageSize, keyword);
            return new BaseList<PublisherModel> { TotalRecords = publisher.TotalRecords, Records = publisher.Records.Select(record => new PublisherModel(record)).ToList() };
        }

        [HttpGet]
        [Route("{id}")]
        public PublisherModel GetPublisher([FromRoute] int id)
        {
            PublisherRepository repo = new PublisherRepository();
            return new PublisherModel(repo.GetById(id));
        }

        [HttpPost]
        [Route("Add")]
        public PublisherModel AddPublisher(PublisherModel publisher)
        {
            PublisherRepository repo = new PublisherRepository();
            return new PublisherModel(repo.Add(publisher.ToEntity()));
        }

        [HttpPut]
        [Route("Update")]
        public PublisherModel UpdatePublisher(PublisherModel publisher)
        {
            PublisherRepository repo = new PublisherRepository();
            return new PublisherModel(repo.Update(publisher.ToEntity()));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public PublisherModel DeletePublisher([FromRoute] int id)
        {
            PublisherRepository repo = new PublisherRepository();
            return new PublisherModel(repo.Delete(id));
        }
    }
}
