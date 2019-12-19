using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Facades;
using Data.Services;
using Data;
using Presentation.ViewModels;
using System.Threading;
using Presentation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class DataContextTest
    {
        DBContextDataContext db;
        ClientSrv clientSrv;
        BookSrv bookSrv;
        EventSrv eventSrv;
        SupplierSrv supplierSrv;
        UserFcd userFcd;
        TestEditBookWindow editBookWindow;
        TestEditClientWindow editClientWindow;
        TestAddClientWindow addClientWindow;
        TestEventLogsWindow eventLogsWindow;
        TestAddBookWindow addBookWindow;
        MainViewModel mvm;


        [TestInitialize]
        public void InitializeDataContext()
        {
            db = new DBContextDataContext("Data Source=83.29.107.71\\\\SQLEXPRESS,1433;Initial Catalog=BookstoreDB_TEST;User ID=admin;Password=adminpassword1");
            //db = new DBContextDataContext();
            eventSrv = new EventSrv(db);
            clientSrv = new ClientSrv(db);
            bookSrv = new BookSrv(db);
            supplierSrv = new SupplierSrv(db);
            userFcd = new UserFcd(bookSrv, clientSrv, eventSrv, supplierSrv);
            editBookWindow = new TestEditBookWindow();
            editClientWindow = new TestEditClientWindow();
            addClientWindow = new TestAddClientWindow();
            eventLogsWindow = new TestEventLogsWindow();
            addBookWindow = new TestAddBookWindow();
            mvm = new MainViewModel(userFcd, editClientWindow, editBookWindow, addClientWindow, eventLogsWindow, addBookWindow);
        }

        
        [TestMethod] 
        public void TestAccountBalanceChange()
        {
            bool accountBalanceChanged = false;
            mvm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AccountBalance")
                    accountBalanceChanged = true;
            };

            mvm.AccountBalance += 1000;
            Assert.AreEqual(accountBalanceChanged, true);
        }    
               

        [TestMethod]
        public void ClientsAddedAndDeletedTest()
        {
            
            int amountOfClientsAtTheBeggining = clientSrv.GetClientList().Count;
            clientSrv.CreateClient(new Clients
            {
                c_name = "John",
                c_surname = "Smith"
            });

            Assert.AreEqual(clientSrv.GetClientList().Count - amountOfClientsAtTheBeggining, 1);

            int currentAmount = clientSrv.GetClientList().Count;
            var toDelete = clientSrv.GetClientList().Where(x => x.c_name == "John" && x.c_surname == "Smith").SingleOrDefault();
            clientSrv.DeleteClient(toDelete.id);

            Assert.AreEqual(currentAmount - clientSrv.GetClientList().Count, 1);
        }

        [TestMethod]
        public void SuppliersAddedAndDeletedTest()
        {
            int amountOfSuppliersAtTheBeggining = supplierSrv.GetSupplierList().Count;
            supplierSrv.CreateSupplier(new Suppliers
            {
                s_name = "Green Owl",
                nip = "1234567890"
            });

            Assert.AreEqual(supplierSrv.GetSupplierList().Count - amountOfSuppliersAtTheBeggining, 1);

            int currentAmount = supplierSrv.GetSupplierList().Count;
            var toDelete = supplierSrv.GetSupplierList().Where(x => x.s_name == "Green Owl").SingleOrDefault();
            supplierSrv.DeleteSupplier(toDelete.id);

            Assert.AreEqual(currentAmount - supplierSrv.GetSupplierList().Count, 1);

        }

        [TestMethod]
        public void BooksBoughtAndSoldTest()
        {
 

            int countbefore = bookSrv.GetBookList().Count;

            bookSrv.CreateBook(new Books
            {
                amount = 33,
                b_author = "Stephen King",
                isNew = false,
                b_name = "The Shining",
                price = 20.00f,
                supplierID = 1
            });

            Assert.AreEqual(bookSrv.GetBookList().Count - countbefore, 1);

            var toDelete = bookSrv.GetBookList().Where(x => x.b_name == "The Shining").SingleOrDefault();

            bookSrv.UpdateBook(new Books
            {
                amount = 33,
                b_author = "Stephen King",
                isNew = false,
                id = toDelete.id,
                b_name = "The Shining",
                price = 24.00f,
                supplierID = 1
            });

            Assert.AreEqual(24.00f, bookSrv.GetBook(4).price);

            int count = bookSrv.GetBookList().Count;
            bookSrv.DeleteBook(toDelete.id);

            Assert.AreEqual(count - bookSrv.GetBookList().Count, 1);

        }
        [TestMethod]
        public async Task TestOfFacade()
        {
            Books book = new Books
            {
                b_name = "Tożsamość Bourne'a",
                b_author = "Robert Ludlum",
                amount = 2,
                isNew = true,
                price = 30.99f,
                supplierID = 1
            };

            int titlesBeforePurchase = bookSrv.GetBookList().Count;
            float accountBalanceBeforePurchase = eventSrv.GetAccountBalance();
            await userFcd.BuyBook(book);
            Assert.AreEqual(bookSrv.GetBookList().Count - titlesBeforePurchase, 1);
            Assert.AreEqual(accountBalanceBeforePurchase - eventSrv.GetAccountBalance() , (float)(book.price * book.amount), delta: 0.01f);

        }
        
        
    }
    
}
