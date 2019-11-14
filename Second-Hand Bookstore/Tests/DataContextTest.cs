using System;
using Data.DataContext;
using Logic.Services;
using Data.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Facades;

namespace Tests
{
    [TestClass]
    public class DataContextTest
    {
        DataContext db;
        ClientSrv clientSrv;
        BookSrv bookSrv;
        EventSrv eventSrv;
        SupplierSrv supplierSrv;
        UserFcd userFcd;

        [TestInitialize]
        public void InitializeDataContext()
        {
            db = new DataContext();
            eventSrv = new EventSrv(db);
            clientSrv = new ClientSrv(db);
            bookSrv = new BookSrv(db, eventSrv, clientSrv);
            supplierSrv = new SupplierSrv(db);

            userFcd = new UserFcd(bookSrv, clientSrv, eventSrv, supplierSrv);

            // Adding initial Suppliers
            supplierSrv.CreateSupplier(new tSupplier
            {
                Id = 0,
                Name = "Empik",
                NIP = "9406723512",
                CreationDate = new DateTime(2019, 5, 10)
            });
            supplierSrv.CreateSupplier(new tSupplier
            {
                Id = 1,
                Name = "Nowa Era",
                NIP = "8295829592",
                CreationDate = new DateTime(2019, 2, 5)
            });


            // Adding initial books

            bookSrv.CreateBook(new tBook
            {
                Id = 0,
                Name = "Harry Potter",
                Author = "J. K. Rowling",
                Amount = 55,
                isNew = true,
                Price = 39.99f,
                Supplier = supplierSrv.GetSupplier(0)
            });
            bookSrv.CreateBook(new tBook
            {
                Id = 1,
                Name = "50 Shades of Gray",
                Author = "E. L. James",
                Amount = 43,
                isNew = true,
                Price = 15.0f,
                Supplier = supplierSrv.GetSupplier(1)
            });
            bookSrv.CreateBook(new tBook
            {
                Id = 2,
                Name = "Harry Potter",
                Author = "J. K. Rowling",
                Amount = 12,
                isNew = false,
                Price = 19.99f,
                Supplier = supplierSrv.GetSupplier(1)
            });
            bookSrv.CreateBook(new tBook
            {
                Id = 3,
                Name = "Lord of the Rings",
                Author = "J. R. R. Tolkien",
                Amount = 92,
                isNew = true,
                Price = 35.99f,
                Supplier = supplierSrv.GetSupplier(0)
            });

            // Adding initial Client
            clientSrv.CreateClient(new tClient
            {
                Id = 0,
                Name = "Jan",
                Surname = "Kowalski",
                CreationDate = new DateTime(2018, 12, 12)
            });

            // Adding initial Event with initial account balance
            eventSrv.RegisterEvent(new tEvent
            {
                AccountBalance = 10000.0f,
                Book = null,
                EventTime = DateTime.Now,
                Id = 0
            });

        }

        [TestMethod]
        public void ClientsAddedAndDeletedTest()
        {
            
            int amountOfClientsAtTheBeggining = clientSrv.GetClientList().Count;
            clientSrv.CreateClient(new tClient
            {
                Id = 1,
                Name = "John",
                Surname = "Smith",
                CreationDate = DateTime.Now
            });

            Assert.AreEqual(clientSrv.GetClientList().Count - amountOfClientsAtTheBeggining, 1);

            int currentAmount = clientSrv.GetClientList().Count;
            clientSrv.DeleteClient(1);

            Assert.AreEqual(currentAmount - clientSrv.GetClientList().Count, 1);
        }

        [TestMethod]
        public void SuppliersAddedAndDeletedTest()
        {
            int amountOfSuppliersAtTheBeggining = supplierSrv.GetSupplierList().Count;
            supplierSrv.CreateSupplier(new tSupplier
            {
                Id = 20,
                Name = "Green Owl",
                NIP = "1234567890",
                CreationDate = DateTime.Now
            });

            Assert.AreEqual(supplierSrv.GetSupplierList().Count - amountOfSuppliersAtTheBeggining, 1);

            int currentAmount = supplierSrv.GetSupplierList().Count;
            supplierSrv.DeleteSupplier(20);

            Assert.AreEqual(currentAmount - supplierSrv.GetSupplierList().Count, 1);

        }

        [TestMethod]
        public void BooksBoughtAndSoldTest()
        {
            tSupplier testSupplier = new tSupplier
            {
                CreationDate = DateTime.Now,
                Id = 2,
                Name = "Nowa Era",
                NIP = "12345678"
            };
            Assert.AreEqual(4, bookSrv.GetBookList().Count);

            bookSrv.CreateBook(new tBook
            {
                Amount = 33,
                Author = "Stephen King",
                Id = 4,
                isNew = false,
                Name = "The Shining",
                Price = 20.00f,
                Supplier = testSupplier
            });

            Assert.AreEqual(5, bookSrv.GetBookList().Count);

            bookSrv.DeleteBook(2);

            Assert.AreEqual(4, bookSrv.GetBookList().Count);

            bookSrv.UpdateBook(new tBook
            {
                Amount = 33,
                Author = "Stephen King",
                Id = 4,
                isNew = false,
                Name = "The Shining",
                Price = 24.00f,
                Supplier = testSupplier
            });

            Assert.AreEqual(24.00f, bookSrv.GetBook(4).Price);

        }
        [TestMethod]
        public void TestOfFacade()
        {
            tSupplier supplier = new tSupplier
            {
                Id = 10,
                CreationDate = DateTime.Now,
                Name = "New Delivery Company",
                NIP = "18234323"
            };
            tBook book = new tBook
            {
                Id = 20,
                Name = "Tożsamość Bourne'a",
                Author = "Robert Ludlum",
                Amount = 50,
                isNew = true,
                Price = 30.99f,
                Supplier = supplier
            };

            int titlesBeforePurchase = bookSrv.GetBookList().Count;
            float accountBalanceBeforePurchase = eventSrv.GetAccountBalance();
            userFcd.BuyBook(book);
            Assert.AreEqual(bookSrv.GetBookList().Count - titlesBeforePurchase, 1);
            Assert.AreEqual(accountBalanceBeforePurchase - eventSrv.GetAccountBalance() , book.Price * book.Amount);

            int AmountOfBooksPurchase = bookSrv.GetBook(0).Amount;
            float accountBalanceBeforeSelling = eventSrv.GetAccountBalance();
            userFcd.SellBook(0, 0);
            Assert.AreEqual(AmountOfBooksPurchase - bookSrv.GetBook(0).Amount, 1);
            Assert.AreEqual(eventSrv.GetAccountBalance() - accountBalanceBeforeSelling, bookSrv.GetBook(0).Price, 0.01);
        }
    }
}
