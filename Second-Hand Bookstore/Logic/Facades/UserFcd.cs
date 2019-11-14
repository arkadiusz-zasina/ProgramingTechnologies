using Data.DataModels;
using Data.Interfaces;
using Logic.Interfaces;
using System;

namespace Logic.Facades
{
    public class UserFcd : IUserFcd
    {
        IBookSrv _bookSrv;
        IClientSrv _clientSrv;
        IEventSrv _eventSrv;
        ISupplierSrv _supplierSrv;

        public UserFcd(IBookSrv bookSrv, IClientSrv clientSrv, IEventSrv eventSrv, ISupplierSrv supplierSrv)
        {
            _bookSrv = bookSrv;
            _clientSrv = clientSrv;
            _eventSrv = eventSrv;
            _supplierSrv = supplierSrv;
        }

        public void BuyBook(tBook book)
        {
            var bookInDatabase = _bookSrv.GetBook(book.Id);
            var isBought = bookInDatabase != null;

            if (isBought)
            {
                _bookSrv.UpdateBook(new tBook
                {
                    Amount = bookInDatabase.Amount + book.Amount,
                    Author = book.Author,
                    Id = book.Id,
                    isNew = book.isNew,
                    Name = book.Name,
                    Price = book.Price,
                    Supplier = book.Supplier
                });
            }
            else
            {
                _bookSrv.CreateBook(new tBook
                {
                    Amount = book.Amount,
                    Author = book.Author,
                    Id = book.Id,
                    isNew = book.isNew,
                    Name = book.Name,
                    Price = book.Price,
                    Supplier = book.Supplier
                });
            }

            _eventSrv.RegisterEvent(new tBookBoughtEvent
            {
                EventTime = DateTime.Now,
                AccountBalance = _eventSrv.GetAccountBalance() - (book.Price * book.Amount),
                Book = book,
                Id = _eventSrv.GetLastId() + 1,
                Supplier = book.Supplier
            });
        }

        public void GenerateShopRaport()
        {
            Console.WriteLine("Current shop account balance: " + _eventSrv.GetAccountBalance());

            var numberOfBooks = 0;
            foreach(var book in _bookSrv.GetBookList())
            {
                numberOfBooks += book.Amount;
            }

            Console.WriteLine("Number of titles: " + _bookSrv.GetBookList().Count);
            Console.WriteLine("Total number of books: " + numberOfBooks);
            Console.WriteLine("Number of clients: " + _clientSrv.GetClientList().Count);
            Console.WriteLine("Number of suppliers: " + _supplierSrv.GetSupplierList().Count);
        }

        public void SellBook(int bookId, int clientId)
        {
            var book = _bookSrv.GetBook(bookId);
            var isLast = book.Amount == 1;

            if (!isLast)
            {
                _bookSrv.UpdateBook(new tBook
                {
                    Amount = book.Amount - 1,
                    Author = book.Author,
                    Id = book.Id,
                    isNew = book.isNew,
                    Name = book.Name,
                    Price = book.Price,
                    Supplier = book.Supplier
                });
            }
            else
            {
                _bookSrv.DeleteBook(bookId);
            }

            _eventSrv.RegisterEvent(new tBookSoldEvent
            {
                EventTime = DateTime.Now,
                AccountBalance = _eventSrv.GetAccountBalance() + book.Price,
                Book = book,
                Id = _eventSrv.GetLastId() + 1,
                Client = _clientSrv.GetClient(clientId)
            });
        }

        public void ShowAvailibleBooks()
        {
            foreach(var book in _bookSrv.GetBookList())
            {
                Console.WriteLine("Title: " + book.Name + " | Price: " + book.Price + " | Amount: " + book.Amount);
            }
        }

        public void ShowBooksOfSupplier(string supplierName)
            => _bookSrv.GetBooksBySupplier(supplierName);
    }
}
