using System;
using Data.DataContext;
using Data.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Facades;
using Data.Services;

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
            FileFiller fileFiller = new FileFiller();
            StaticFiller staticFiller = new StaticFiller();
            db = new DataContext(fileFiller);
            db.Filler = staticFiller;
            eventSrv = new EventSrv(db);
            clientSrv = new ClientSrv(db);
            bookSrv = new BookSrv(db, eventSrv, clientSrv);
            supplierSrv = new SupplierSrv(db);
            userFcd = new UserFcd(bookSrv, clientSrv, eventSrv, supplierSrv);
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
                NIP = "1234567890"
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
                Id = 2,
                Name = "Nowa Era",
                NIP = "12345678"
            };

            int countbefore = bookSrv.GetBookList().Count;

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

            Assert.AreEqual(bookSrv.GetBookList().Count - countbefore, 1);

            int count = bookSrv.GetBookList().Count;
            bookSrv.DeleteBook(2);

            Assert.AreEqual(count - bookSrv.GetBookList().Count, 1);

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
