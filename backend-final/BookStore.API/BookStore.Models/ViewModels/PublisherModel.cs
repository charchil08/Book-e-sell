using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Data;

namespace BookStore.Models.ViewModels
{
    public class PublisherModel
    {
        public PublisherModel()
        {

        }
        public PublisherModel(Publisher publisher)
        {
            this.Id = publisher.Id;
            this.Name = publisher.Name;
            this.Address = publisher.Address;
            this.Contact = publisher.Contact;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public Publisher ToEntity()
        {
            return new Publisher
            {
                Contact = this.Contact,
                Address = this.Address,
                Name = this.Name,
                Id = this.Id
            };
        }
    }
}
