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
            db = new DBContextDataContext("Data Source=83.29.16.132\\\\SQLEXPRESS,1433;Initial Catalog=BookstoreDB_TEST;User ID=admin;Password=adminpassword1");
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
        public void RelayCommandTesting()
        {
            int executeCount = 0;
            RelayCommand testCommand = new RelayCommand(() => executeCount++);
            Assert.IsTrue(testCommand.CanExecute(null));
            testCommand.Execute(null);
            Assert.AreEqual(1, executeCount);
        }

        [TestMethod]
        public void addBookCommandTest()
        {
            mvm.BookToBeCreated = new Book
            {
                Amount = 20,
                Author = "11111",
                Name = "temporaryBook",
                isNew = true,
                Price = 50,
                 Supplier = new Supplier
                 {
                     id = 1,
                     nip = "2345",
                     s_name = "fdjrfj"
                 }
                
            };
            Assert.IsTrue(mvm.AddBook.CanExecute(null));
            
            int count = mvm.Books.ToList().Count;
            Assert.IsTrue(mvm.AddBook.CanExecute(null));
            mvm.AddBook.Execute(null);
            Thread.Sleep(3000);
            mvm.RefreshBooksSync();
            Assert.AreEqual(count + 1, mvm.Books.ToList().Count);

            db.Books.DeleteAllOnSubmit(db.Books.Where(x => x.b_name == "temporaryBook"));
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void addClientCommandTest()
        {
            mvm.ClientToBeCreated = new Client
            {
                c_name = "temporaryClient",
                c_surname = "temporaryClient",
                creationDate = DateTime.Now
            };
            Assert.IsTrue(mvm.AddClient.CanExecute(null));

            int count = mvm.Clients.ToList().Count;
            Assert.IsTrue(mvm.AddClient.CanExecute(null));
            mvm.AddClient.Execute(null);
            Thread.Sleep(3000);
            mvm.RefreshClientsSync();
            Assert.AreEqual(count + 1, mvm.Clients.ToList().Count);

            db.Clients.DeleteAllOnSubmit(db.Clients.Where(x => x.c_name == "temporaryClient"));
            Thread.Sleep(3000);
        }


        [TestMethod]
        public void deleteBookCommandTest()
        {
            mvm.CurrentBook = new Book
            {
                Amount = 20,
                Author = "11111",
                Name = "temporaryBook",
                isNew = true,
                Price = 50,
                Supplier = new Supplier
                {
                    id = 1,
                    nip = "2345",
                    s_name = "fdjrfj"
                }
            };
            mvm.AddBook.Execute(null);
            Thread.Sleep(3000);
            mvm.RefreshBooksSync();
            int count = mvm.Books.ToList().Count;
            int idToDelete = mvm.Books.FirstOrDefault(x => x.Name == "temporaryBook").Id;
            mvm.CurrentBook.Id = idToDelete;
            Assert.IsTrue(mvm.DeleteBook.CanExecute(null));
            mvm.DeleteBook.Execute(null);
            Thread.Sleep(3000);
            mvm.RefreshBooksSync();
            Assert.AreEqual(count - 1, mvm.Books.ToList().Count);
        }


        [TestMethod]

        public void UpdateBookCommandTest()
        {
            mvm.BookToBeCreated = new Book
            {
                Amount = 20,
                Author = "11111",
                Name = "temporaryBook",
                isNew = true,
                Price = 50,
                Supplier = new Supplier
                {
                    id = 1,
                    nip = "2345",
                    s_name = "fdjrfj"
                }
            };

            mvm.AddBook.Execute(null);
            Thread.Sleep(3000);
            mvm.RefreshBooksSync();
            int idToUpdateBook = mvm.Books.FirstOrDefault(x => x.Name == "temporaryBook").Id;
            string title = mvm.Books.FirstOrDefault(x => x.Id == idToUpdateBook).Name;
            mvm.CurrentBook = mvm.Books.FirstOrDefault(x => x.Id == idToUpdateBook);
            mvm.CurrentBook.Name = "temporaryBook1";
            Assert.IsTrue(mvm.EditBook.CanExecute(null));
            mvm.EditBook.Execute(null);
            Thread.Sleep(3000);
            string title1 = mvm.Books.FirstOrDefault(x => x.Id == idToUpdateBook).Name;

            Assert.AreNotEqual(title, title1);
            db.Books.DeleteAllOnSubmit(db.Books.Where(x => x.b_name == "temporaryBook1"));
        }

        [TestMethod]

        public void UpdateClientCommandTest()
        {
            mvm.ClientToBeCreated = new Client
            {
                c_name = "temporaryClient",
                c_surname = "temporaryClient",
                creationDate = DateTime.Now
            };

            mvm.AddClient.Execute(null);
            Thread.Sleep(3000);
            mvm.RefreshClientsSync();
            int idToUpdateClient = mvm.Clients.FirstOrDefault(x => x.c_name == "temporaryClient").id;
            string name = mvm.Clients.FirstOrDefault(x => x.id == idToUpdateClient).c_name;
            mvm.CurrentClient = mvm.Clients.FirstOrDefault(x => x.id == idToUpdateClient);
            mvm.CurrentClient.c_name = "temporaryClient1";
            Assert.IsTrue(mvm.EditClient.CanExecute(null));
            mvm.EditClient.Execute(null);
            Thread.Sleep(3000);
            mvm.RefreshClientsSync();
            string name1 = mvm.Clients.FirstOrDefault(x => x.id == idToUpdateClient).c_name;

            Assert.AreNotEqual(name, name1);
            db.Clients.DeleteAllOnSubmit(db.Clients.Where(x => x.c_name == "temporaryClient1"));
        }
    }
    
}
