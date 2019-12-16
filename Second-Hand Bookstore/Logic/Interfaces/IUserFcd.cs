using Data;
using Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IUserFcd
    {
        void GenerateShopRaport();
        Task SellBook(int bookId, int clientId);
        void BuyBook(Books book);
        void ShowBooksOfSupplier(string supplierName);
        void ShowAvailibleBooks();
        Task<List<Books>> GetAllBooks();
        Task<IEnumerable<Books>> GetBooksByString(string seraching);
        Task<IEnumerable<Clients>> GetClientsByString(string searching);
        Task<float> GetAccountBalance();
        Task RegisterClient(Clients client);
        Task AddBook(Books book);
        Task<Boolean> isSupplierAvailable(int id);
        Task DeleteBook(int id);
        Task UpdateBook(Books book);
        Task<Books> GetBook(int id);
        Task<Clients> GetClient(int id);
        Task UpdateClient(Clients client);
        Task DeleteClient(int id);
    }
}
