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

        public void BuyBook(tBook book)
        {
            // to be implemented
            var existingBook = datacontext.Books.SingleOrDefault(x => x.Id == book.Id);
            if (existingBook != null)
                existingBook.Amount += book.Amount;
            else
                datacontext.Books.Add(book);
            //pay for book
            //register event
            _eventSrv.RegisterEvent(new tBookBoughtEvent
            {
                EventTime = DateTime.Now,
                AccountBalance = _eventSrv.GetAccountBalance() - book.Price,
                Book = book,
                Id = _eventSrv.GetLastId() + 1,
                Supplier = book.Supplier
            }) ;
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
            return datacontext.Books.Single(x => x.Id == id);
        }

        public List<tBook> GetBookList()
        {
            return datacontext.Books;
        }

        public void SellBook(int bookId, int clientId)
        {
            // to be implemented
            var bookToBeSold = datacontext.Books.Single(x => x.Id == bookId);
            if (bookToBeSold.Amount > 1) bookToBeSold.Amount--;
            //pay for book!!
            //event

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
