using System;
using Data.Database;
using Logic.Services;
using Data.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void ClientsAddedAndDeletedTest()
        {
            Database db = new Database();
            ClientSrv clientSrv = new ClientSrv(db);
            int amountOfClientsAtTheBeggining = clientSrv.GetClientList().Count;
            clientSrv.CreateClient(new tClient
            {
                Id = 1,
                Name = "John",
                Surname = "Smith",
                CreationDate = DateTime.Now
            }) ;

            Assert.AreEqual(clientSrv.GetClientList().Count - amountOfClientsAtTheBeggining, 1);

            int currentAmount = clientSrv.GetClientList().Count;
            clientSrv.DeleteClient(1);

            Assert.AreEqual(currentAmount - clientSrv.GetClientList().Count, 1);
        }

        [TestMethod]
        public void SuppliersAddedAndDeletedTest()
        {
            Database db = new Database();

            SupplierSrv supplierSrv = new SupplierSrv(db);
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
            Database db = new Database();
            EventSrv eventSrv = new EventSrv(db);
            BookSrv bookSrv = new BookSrv(db, eventSrv);

            Assert.AreEqual(4, bookSrv.GetBookList().Count);

            bookSrv.CreateBook(new tBook
            {
                Amount = 33,
                Author = "Stephen King",
                Id = 4,
                isNew = false,
                Name = "The Shining",
                Price = 20.00f,
                SupplierId = 1
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
                SupplierId = 1
            });

            Assert.AreEqual(24.00f, bookSrv.GetBook(4).Price);

            bookSrv.BuyBook(new tBook
            {
                Amount = 5,
                Author = "Henryk Sienkiewicz",
                Name = "Krzyżacy",
                Id = 5,
                isNew = true,
                Price = 80.00f,
                SupplierId = 1
            });

            Assert.AreEqual(5, bookSrv.GetBookList().Count);
            Assert.AreEqual(2, eventSrv.GetListOfEvents().Count);
            Assert.AreEqual(9920.0f, eventSrv.GetAccountBalance());

        }
    }
}
