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

namespace Tests
{
    [TestClass]
    public class DataContextTest
    {
        TestDBContextDataContext db;
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
            db = new TestDBContextDataContext();
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
        public void TestClientAddition()
        {
            mvm.ClientToBeCreated.c_name = "John";
            mvm.ClientToBeCreated.c_surname = "Smith";
            mvm.addClient();

            Assert.IsNotNull(mvm.ClientToBeCreated);
        }

       [TestMethod]
        public void TestEventsRefresh()
        {
            var previousCount = mvm.Books.ToList().Count();
            Thread.Sleep(3000);
            mvm.BookToBeCreated.Name = "ABC";
            mvm.BookToBeCreated.Author = "abc";
            mvm.BookToBeCreated.Amount = 10;
            mvm.BookToBeCreated.isNew = false;
            mvm.BookToBeCreated.Supplier = new Supplier { 
                nip = "12345", 
                s_name = "abcd" };
            mvm.addBook();

            Assert.AreEqual(previousCount + 1, mvm.Books.ToList().Count());

        }

       
       
        
/*
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
        */
        
    }
    
}
