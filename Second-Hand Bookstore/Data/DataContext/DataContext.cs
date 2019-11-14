using Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContext
{
    public class DataContext
    {
        public List<tBook> Books { get; set; }
        public List<tClient> Clients { get; set; }
        public List<tSupplier> Suppliers { get; set; }
        public List<tEvent> Events { get; set; }

        public DataContext()
        {
            Books = new List<tBook>();
            Clients = new List<tClient>();
            Suppliers = new List<tSupplier>();
            Events = new List<tEvent>();

           

        }
    }
}
