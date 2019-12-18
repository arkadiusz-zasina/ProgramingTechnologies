using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Facades;
using Data.Services;
using Data;

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

        [TestInitialize]
        public void InitializeDataContext()
        {
            db = new DBContextDataContext();
            eventSrv = new EventSrv(db);
            clientSrv = new ClientSrv(db);
            bookSrv = new BookSrv(db);
            supplierSrv = new SupplierSrv(db);
            userFcd = new UserFcd(bookSrv, clientSrv, eventSrv, supplierSrv);
        }

        

 //       [TestMethod] 

    
    }
}
