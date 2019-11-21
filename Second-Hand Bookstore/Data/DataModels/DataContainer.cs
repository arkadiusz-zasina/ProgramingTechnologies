using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class DataContainer
    {
        public List<tBook> Books { get; set; }
        public List<tClient> Clients { get; set; }
        public List<tSupplier> Suppliers { get; set; }
        public List<tEvent> Events { get; set; }

        public DataContainer(List<tBook> books, List<tClient> clients, List<tSupplier> suppliers, List<tEvent> events)
        {
            this.Books = books;
            this.Clients = clients;
            this.Suppliers = suppliers;
            this.Events = events;
        }
    }
}
