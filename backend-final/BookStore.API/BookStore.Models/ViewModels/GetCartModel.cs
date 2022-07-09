using BookStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class GetCartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public BookModel Book { get; set; }
        public int Quantity { get; set; }
    }
}
