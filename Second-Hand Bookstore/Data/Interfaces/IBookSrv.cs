using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBookSrv
    {
        void CreateBook(Books book);
        Books GetBook(int id);
        void UpdateBook(Books book);
        void DeleteBook(int id);
        List<Books> GetBookList();
        List<Books> GetBooksBySupplier(string supplierName);
        IEnumerable<Books> GetBooksByString(string searched);
    }
}
