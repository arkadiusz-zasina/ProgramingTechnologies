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
        void SellBook(int bookId, int clientId);
        void BuyBook(tBook book, int amount);
        void ShowBooksOfSupplier(string supplierName);
        void ShowAvailibleBooks();
    }
}
