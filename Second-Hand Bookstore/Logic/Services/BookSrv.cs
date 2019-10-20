using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Database;
using Data.DataModels;
using Logic.Interfaces;

namespace Logic.Services
{
    public class BookSrv : IBookSrv
    {
        Database database;
        IEventSrv _eventSrv;

        public BookSrv(Database database, IEventSrv _eventSrv)
        {
            this.database = database;
            this._eventSrv = _eventSrv;
        }

        public void BuyBook(tBook book)
        {
            // to be implemented
            var existingBook = database.Books.SingleOrDefault(x => x.Id == book.Id);
            if (existingBook != null)
                existingBook.Amount += book.Amount;
            else
                database.Books.Add(book);
            //pay for book
            //register event
            _eventSrv.RegisterEvent(new tBookBoughtEvent
            {
                EventTime = DateTime.Now,
                AccountBalance = _eventSrv.GetAccountBalance() - book.Price,
                BookId = book.Id,
                Id = _eventSrv.GetLastId() + 1,
                SupplierId = book.SupplierId
            }) ;
        }

        public void CreateBook(tBook book)
        {
            database.Books.Add(book);
        }

        public void DeleteBook(int id)
        {
            database.Books.RemoveAll(x => x.Id == id);
        }

        public tBook GetBook(int id)
        {
            return database.Books.Single(x => x.Id == id);
        }

        public List<tBook> GetBookList()
        {
            return database.Books;
        }

        public void SellBook(int bookId, int clientId)
        {
            // to be implemented
            var bookToBeSold = database.Books.Single(x => x.Id == bookId);
            if (bookToBeSold.Amount > 1) bookToBeSold.Amount--;
            //pay for book!!
            //event

            _eventSrv.RegisterEvent(new tBookSoldEvent
            {
                EventTime = DateTime.Now,
                AccountBalance = _eventSrv.GetAccountBalance() + bookToBeSold.Price,
                BookId = bookToBeSold.Id,
                Id = _eventSrv.GetLastId() + 1,
                ClientId = clientId
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
