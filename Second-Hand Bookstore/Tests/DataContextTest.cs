using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Facades;
using Data.Services;
using Data;
using Presentation.ViewModels;
using System.Threading;

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
            mvm = new MainViewModel(userFcd, editClientWindow, editBookWindow, addClientWindow, eventLogsWindow);
            
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

    /*    [TestMethod]
        public void TestEventsRefresh()
        {
            mvm.getListOfEvents();
            Thread.Sleep(3000);
            Assert.IsNotNull(mvm.Events);
        }*/
    }
}
