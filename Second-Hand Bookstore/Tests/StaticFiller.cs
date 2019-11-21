using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModels;
using Data.Interfaces;

namespace Tests
{
    class StaticFiller : IFiller
    {
        public DataContainer Fill()
        {
            List<tBook> books = new List<tBook>();
            List<tClient> clients = new List<tClient>();
            List<tSupplier> suppliers = new List<tSupplier>();
            List<tEvent> events = new List<tEvent>();

            // Adding initial Suppliers
            suppliers.Add(new tSupplier
            {
                Id = 0,
                Name = "Empik",
                NIP = "9406723512"
            });
            suppliers.Add(new tSupplier
            {
                Id = 1,
                Name = "Nowa Era",
                NIP = "8295829592"
            });


            // Adding initial books

            books.Add(new tBook
            {
                Id = 0,
                Name = "Harry Potter",
                Author = "J. K. Rowling",
                Amount = 55,
                isNew = true,
                Price = 39.99f,
                Supplier = suppliers.SingleOrDefault(x => x.Id == 0)
            }) ;
            books.Add(new tBook
            {
                Id = 1,
                Name = "50 Shades of Gray",
                Author = "E. L. James",
                Amount = 43,
                isNew = true,
                Price = 15.0f,
                Supplier = suppliers.SingleOrDefault(x => x.Id == 1)
            });
            books.Add(new tBook
            {
                Id = 2,
                Name = "Harry Potter",
                Author = "J. K. Rowling",
                Amount = 12,
                isNew = false,
                Price = 19.99f,
                Supplier = suppliers.SingleOrDefault(x => x.Id == 1)
            });

            books.Add(new tBook
            {
                Id = 3,
                Name = "Lord of the Rings",
                Author = "J. R. R. Tolkien",
                Amount = 92,
                isNew = true,
                Price = 35.99f,
                Supplier = suppliers.SingleOrDefault(x => x.Id == 0)
            });

            // Adding initial Client
            clients.Add(new tClient
            {
                Id = 0,
                Name = "Jan",
                Surname = "Kowalski",
                CreationDate = new DateTime(2018, 12, 12)
            });

            // Adding initial Event with initial account balance
            events.Add(new tEvent
            {
                AccountBalance = 10000.0f,
                Book = null,
                EventTime = DateTime.Now,
                Id = 0
            });

            return new DataContainer(books, clients, suppliers, events);

        }
    }
}
