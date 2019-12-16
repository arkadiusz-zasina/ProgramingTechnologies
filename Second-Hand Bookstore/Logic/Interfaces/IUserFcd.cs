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
        Task<float> GetAccountBalance();
    }
}
