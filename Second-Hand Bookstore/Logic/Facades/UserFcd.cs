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

        public async Task BuyBook(Books book)
        {
            var bookInDatabase = await Task.Run(() => _bookSrv.GetBook(book.id));
            var isBought = bookInDatabase != null;
            int obtainedBookId;

            if (isBought)
            {
                await Task.Run(() => _bookSrv.UpdateBook(new Books
                {
                    amount = bookInDatabase.amount + book.amount,
                    b_author = book.b_author,
                    id = book.id,
                    isNew = book.isNew,
                    b_name = book.b_name,
                    price = book.price,
                    supplierID = book.supplierID
                }));
                obtainedBookId = bookInDatabase.id;
            }
            else
            {
                var added = new Books
                {
                    amount = book.amount,
                    b_author = book.b_author,
                    id = book.id,
                    isNew = book.isNew,
                    b_name = book.b_name,
                    price = book.price,
                    supplierID = book.supplierID
                };
                await Task.Run(() => _bookSrv.CreateBook(added));
                obtainedBookId = added.id;
            }

            await Task.Run(() => _eventSrv.RegisterEvent(new Events
            {
                event_time = DateTime.Now,
                account_balance = _eventSrv.GetAccountBalance() - (book.price * book.amount),
                book_id = obtainedBookId,
                supplier_id = book.supplierID
            }));
        }


        public async Task<List<Books>> GetAllBooks()
        {
            return await Task.Run(() => _bookSrv.GetBookList());
        }

        public List<Books> GetAllBooksSync()
        {
            return _bookSrv.GetBookList();
        }

        public List<Clients> GetAllClientsSync()
        {
            return _clientSrv.GetClientList();
        }

        public async Task SellBook(int bookId, int clientId)
        {
            var book = _bookSrv.GetBook(bookId);

            await Task.Run(() => _bookSrv.UpdateBook(new Books
            {
                amount = book.amount - 1,
                b_author = book.b_author,
                id = book.id,
                isNew = book.isNew,
                b_name = book.b_name,
                price = book.price,
                supplierID = book.supplierID
            }));

            await Task.Run(() => _eventSrv.RegisterEvent(new Events
            {
                event_time = DateTime.Now,
                account_balance = _eventSrv.GetAccountBalance() + book.price,
                book_id = book.id,
                id = _eventSrv.GetLastId() + 1,
                client_id = _clientSrv.GetClient(clientId).id
            }));
        }

        public void ShowBooksOfSupplier(string supplierName)
            => _bookSrv.GetBooksBySupplier(supplierName);

        public async Task<IEnumerable<Books>> GetBooksByString(string seraching)
            => await Task.Run(() => _bookSrv.GetBooksByString(seraching));

        public async Task<float> GetAccountBalance()
            => await Task.Run(() => _eventSrv.GetAccountBalance());

        public float GetAccountBalanceSync()
            => _eventSrv.GetAccountBalance();


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

        public async Task<List<Events>> GetListOfEvents()
            => await Task.Run(() => _eventSrv.GetListOfEvents());

        public async Task<List<Suppliers>> GetListOfSuppliers()
            => await Task.Run(() => _supplierSrv.GetSupplierList());

    }
}
