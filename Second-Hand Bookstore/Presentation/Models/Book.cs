using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Author { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public Boolean isNew { get; set; }
    }
}
