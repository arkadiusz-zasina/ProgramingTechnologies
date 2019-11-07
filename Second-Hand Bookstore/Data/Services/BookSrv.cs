using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContext;
using Data.DataModels;
using Logic.Interfaces;

namespace Logic.Services
{
    public class BookSrv : IBookSrv
    {
        private DataContext datacontext;
        IEventSrv _eventSrv;
        IClientSrv _clientSrv;
        public BookSrv(DataContext datacontext, IEventSrv _eventSrv, IClientSrv _clientSrv)
        {
            this.datacontext = datacontext;
            this._eventSrv = _eventSrv;
            this._clientSrv = _clientSrv;
        }

        public void CreateBook(tBook book)
        {
            datacontext.Books.Add(book);
        }

        public void DeleteBook(int id)
        {
            datacontext.Books.RemoveAll(x => x.Id == id);
        }

        public tBook GetBook(int id)
        {
            return datacontext.Books.SingleOrDefault(x => x.Id == id);
        }

        public List<tBook> GetBookList()
        {
            return datacontext.Books;
        }

        public List<tBook> GetBooksBySupplier(string supplierName)
        {
            return datacontext.Books.Where(x => x.Supplier.Name == supplierName).ToList();
        }

        // to fcd
        public void SellBook(int bookId, int clientId)
        {
            var bookToBeSold = datacontext.Books.Single(x => x.Id == bookId);
            if (bookToBeSold.Amount > 1) bookToBeSold.Amount--;

            _eventSrv.RegisterEvent(new tBookSoldEvent
            {
                EventTime = DateTime.Now,
                AccountBalance = _eventSrv.GetAccountBalance() + bookToBeSold.Price,
                Book = bookToBeSold,
                Id = _eventSrv.GetLastId() + 1,
                Client = _clientSrv.GetClient(clientId)
            });

        }

        public void UpdateBook(tBook book)
        {
            DeleteBook(book.Id);
            CreateBook(book);
           // var tempbook = database.Books.Single(x => x.Id == book.Id);
           // tempbook = book;
        }
    }
}
