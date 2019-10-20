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
    class BookSrv : IBookSrv
    {
        Database database;

        public BookSrv(Database database)
        {
            this.database = database;
        }

        public void BuyBook(tBook book)
        {
            // to be implemented
            var existingBook = database.Books.Single(x => x.Name == book.Name);
            if (existingBook != null)
                existingBook.Amount += book.Amount;
            else
                database.Books.Add(book);
            //pay for book
            //register event
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

        public IEnumerable<tBook> GetBookList()
        {
            return database.Books;
        }

        public void SellBook(int id)
        {
            // to be implemented
            var bookToBeSold = database.Books.Single(x => x.Id == id);
            if (bookToBeSold.Amount > 1) bookToBeSold.Amount--;
            //pay for book!!
            //event
        }

        public void UpdateBook(tBook book)
        {
            var tempbook = database.Books.Single(x => x.Id == book.Id);
            tempbook = book;
        }
    }
}
