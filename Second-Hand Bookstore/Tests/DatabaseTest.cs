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
        }

        [TestMethod]
        public void SuppliersAddedAndDeletedTest()
        {
            Database db = new Database();

        }

        [TestMethod]
        public void BooksBoughtAndSoldTest()
        {
            Database db = new Database();
            EventSrv eventSrv = new EventSrv(db);
            BookSrv bookSrv = new BookSrv(db, eventSrv);

            Assert.AreEqual(4, bookSrv.GetBookList().Count);
            
            

        }
    }
}
