using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContext;
using Data.DataModels;
using Data.Interfaces;

namespace Data.Services
{
    public class BookSrv : IBookSrv
    {
        private DBContextDataContext datacontext;
        IEventSrv _eventSrv;
        IClientSrv _clientSrv;
        public BookSrv(DBContextDataContext datacontext, IEventSrv _eventSrv, IClientSrv _clientSrv)
        {
            this.datacontext = datacontext;
            this._eventSrv = _eventSrv;
            this._clientSrv = _clientSrv;
        }

        public void CreateBook(Books book)
        {
            datacontext.Books.InsertOnSubmit(book);
            datacontext.SubmitChanges();
        }

        public void DeleteBook(int id)
        {
            datacontext.Books.DeleteOnSubmit(datacontext.Books.Where(i => i.id == id).Single());
            datacontext.SubmitChanges();
        }

        public Books GetBook(int id)
        {
            return datacontext.Books.SingleOrDefault(x => x.id == id);
        }

        public List<Books> GetBookList()
        {
            return datacontext.Books.ToList();
        }

        public List<Books> GetBooksBySupplier(string supplierName)
        {
            var getids = datacontext.Suppliers.Where(supplier => supplier.s_name == supplierName).Select(supplier => supplier.id).SingleOrDefault();
            var books = datacontext.Books.Where(book => getids == book.supplierID);
            return books.ToList();
        }


        public void UpdateBook(Books book)
        {
            DeleteBook(book.id);
            CreateBook(book);
            // var tempbook = database.Books.Single(x => x.Id == book.Id);
            // tempbook = book;
        }
    }
}
