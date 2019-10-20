﻿using Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class Database
    {
        public List<tBook> Books { get; set; }
        public List<tClient> Clients { get; set; }
        public List<tSupplier> Suppliers { get; set; }
        public List<tEvent> Events { get; set; }

        public Database()
        {
            // Adding initial books
            Books.Add(new tBook
            {
                Id = 0,
                Name = "Harry Potter",
                Author = "J. K. Rowling",
                Amount = 55,
                isNew = true,
                Price = 39.99f,
                SupplierId = 0,
            });
            Books.Add(new tBook
            {
                Id = 1,
                Name = "50 Shades of Gray",
                Author = "E. L. James",
                Amount = 43,
                isNew = true,
                Price = 15.0f,
                SupplierId = 0,
            });
            Books.Add(new tBook
            {
                Id = 2,
                Name = "Harry Potter",
                Author = "J. K. Rowling",
                Amount = 12,
                isNew = false,
                Price = 19.99f,
                SupplierId = 0,
            });
            Books.Add(new tBook
            {
                Id = 3,
                Name = "Lord of the Rings",
                Author = "J. R. R. Tolkien",
                Amount = 92,
                isNew = true,
                Price = 35.99f,
                SupplierId = 1,
            });

            // Adding initial Suppliers
            Suppliers.Add(new tSupplier
            {
                Id = 0,
                Name = "Empik",
                NIP = "9406723512",
                CreationDate = new DateTime(2019, 5, 10)
            });
            Suppliers.Add(new tSupplier
            {
                Id = 1,
                Name = "Nowa Era",
                NIP = "8295829592",
                CreationDate = new DateTime(2019, 2, 5)
            });

            // Adding initial Client
            Clients.Add(new tClient
            {
                Id = 0,
                Name = "Jan",
                Surname = "Kowalski",
                CreationDate = new DateTime(2018, 12, 12)
            });

            // Adding initial Event with initial account balance
            Events.Add(new tEvent
            {
                AccountBalance = 10000.0f,
                BookId = -1,
                EventTime = DateTime.Now,
                Id = 0
            });

        }
    }
}
