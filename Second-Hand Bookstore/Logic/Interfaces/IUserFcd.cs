using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IUserFcd
    {
        Task SellBook(int bookId, int clientId);
        Task BuyBook(Books book);
        void ShowBooksOfSupplier(string supplierName);
        Task<List<Books>> GetAllBooks();
        List<Books> GetAllBooksSync();
        List<Clients> GetAllClientsSync();
        Task<IEnumerable<Books>> GetBooksByString(string seraching);
        Task<IEnumerable<Clients>> GetClientsByString(string searching);
        Task<float> GetAccountBalance();
        float GetAccountBalanceSync();
        Task RegisterClient(Clients client);
        Task AddBook(Books book);
        Task<Boolean> isSupplierAvailable(int id);
        Task DeleteBook(int id);
        Task UpdateBook(Books book);
        Task<Books> GetBook(int id);
        Task<Clients> GetClient(int id);
        Task UpdateClient(Clients client);
        Task DeleteClient(int id);
        Task<List<Events>> GetListOfEvents();
        Task<List<Suppliers>> GetListOfSuppliers();
    }
}
