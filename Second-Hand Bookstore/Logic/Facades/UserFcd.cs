using Data;
using Data.Interfaces;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public void BuyBook(Books book)
        {
            var bookInDatabase = _bookSrv.GetBook(book.id);
            var isBought = bookInDatabase != null;

            if (isBought)
            {
                _bookSrv.UpdateBook(new Books
                {
                    amount = bookInDatabase.amount + book.amount,
                    b_author = book.b_author,
                    id = book.id,
                    isNew = book.isNew,
                    b_name = book.b_name,
                    price = book.price,
                    supplierID = book.supplierID
                });
            }
            else
            {
                _bookSrv.CreateBook(new Books
                {
                    amount = book.amount,
                    b_author = book.b_author,
                    id = book.id,
                    isNew = book.isNew,
                    b_name = book.b_name,
                    price = book.price,
                    supplierID = book.supplierID
                });
            }

            _eventSrv.RegisterEvent(new Events
            {
                event_time = DateTime.Now,
                account_balance = _eventSrv.GetAccountBalance() - (book.price * book.amount),
                book_id = book.id,
                id = _eventSrv.GetLastId() + 1,
                supplier_id = book.supplierID
            });
        }

        public void GenerateShopRaport()
        {
            Console.WriteLine("Current shop account balance: " + _eventSrv.GetAccountBalance());

            var numberOfBooks = 0;
            foreach(var book in _bookSrv.GetBookList())
            {
                numberOfBooks += book.amount.Value;
            }

            Console.WriteLine("Number of titles: " + _bookSrv.GetBookList().Count);
            Console.WriteLine("Total number of books: " + numberOfBooks);
            Console.WriteLine("Number of clients: " + _clientSrv.GetClientList().Count);
            Console.WriteLine("Number of suppliers: " + _supplierSrv.GetSupplierList().Count);
        }

        public async Task<List<Books>> GetAllBooks()
        {
            return await Task.Run(() => _bookSrv.GetBookList());
        }

        public void SellBook(int bookId, int clientId)
        {
            var book = _bookSrv.GetBook(bookId);
            var isLast = book.amount == 1;

            if (!isLast)
            {
                _bookSrv.UpdateBook(new Books
                {
                    amount = book.amount - 1,
                    b_author = book.b_author,
                    id = book.id,
                    isNew = book.isNew,
                    b_name = book.b_name,
                    price = book.price,
                    supplierID = book.supplierID
                });
            }
            else
            {
                _bookSrv.DeleteBook(bookId);
            }

            _eventSrv.RegisterEvent(new Events
            {
                event_time = DateTime.Now,
                account_balance = _eventSrv.GetAccountBalance() + book.price,
                book_id = book.id,
                id = _eventSrv.GetLastId() + 1,
                client_id = _clientSrv.GetClient(clientId).id
            });
        }

        public void ShowAvailibleBooks()
        {
            foreach(var book in _bookSrv.GetBookList())
            {
                Console.WriteLine("Title: " + book.b_name + " | Price: " + book.price + " | Amount: " + book.amount);
            }
        }

        public void ShowBooksOfSupplier(string supplierName)
            => _bookSrv.GetBooksBySupplier(supplierName);

        public async Task<IEnumerable<Books>> GetBooksByString(string seraching)
            => await Task.Run(() => _bookSrv.GetBooksByString(seraching));

        public async Task<float> GetAccountBalance()
            => await Task.Run(() => _eventSrv.GetAccountBalance());

        Task IUserFcd.SellBook(int bookId, int clientId)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterClient(Clients client)
            => await Task.Run(() => _clientSrv.CreateClient(client));

        public async Task AddBook(Books book)
            => await Task.Run(() => _bookSrv.CreateBook(book));

        public async Task<IEnumerable<Clients>> GetClientsByString(string searching)
            => await Task.Run(() => _clientSrv.GetClientsByString(searching));

        public async Task<bool> isSupplierAvailable(int id)
            => await Task.Run(() => _supplierSrv.isSupplierAvailable(id));

        public async Task DeleteBook(int id)
            => await Task.Run(() => _bookSrv.DeleteBook(id));

        public async Task UpdateBook(Books book)
            => await Task.Run(() => _bookSrv.UpdateBook(book));

        public async Task<Books> GetBook(int id)
            => await Task.Run(() => _bookSrv.GetBook(id));

        public async Task<Clients> GetClient(int id)
            => await Task.Run(() => _clientSrv.GetClient(id));

        public async Task UpdateClient(Clients client)
            => await Task.Run(() => _clientSrv.UpdateClient(client));

        public async Task DeleteClient(int id)
            => await Task.Run(() => _clientSrv.DeleteClient(id));
    }
}
