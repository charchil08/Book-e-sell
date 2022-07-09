using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BaseList<T> where T:class
    {
        public List<T> Records { get; set; }
        public int TotalRecords { get; set; }
    }
}
