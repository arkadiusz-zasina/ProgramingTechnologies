using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data.Services
{
    public class BookSrv : IBookSrv
    {
        private IDBContextDataContext datacontext;
        public BookSrv(IDBContextDataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public void CreateBook(Books book)
        {
            datacontext.Books.InsertOnSubmit(book);
            datacontext.SubmitChanges();
        }

        public void DeleteBook(int id)
        {
            datacontext.Books.DeleteOnSubmit(datacontext.Books.Where(i => i.id == id).Single());
            datacontext.SubmitChanges();
        }

        public Books GetBook(int id)
        {
            return datacontext.Books.SingleOrDefault(x => x.id == id);
        }

        public List<Books> GetBookList()
        {
            return datacontext.Books.ToList();
        }

        public List<Books> GetBooksBySupplier(string supplierName)
        {
            var getids = datacontext.Suppliers.Where(supplier => supplier.s_name == supplierName).Select(supplier => supplier.id).SingleOrDefault();
            var books = datacontext.Books.Where(book => getids == book.supplierID);
            return books.ToList();
        }


        public void UpdateBook(Books book)
        {
            var toupdate = from b in datacontext.Books
                           where b.id == book.id
                           select b;

            foreach(Books b in toupdate)
            {
                b.b_name = book.b_name;
                b.b_author = book.b_author;
                b.price = book.price;
                b.amount = book.amount;
                b.isNew = book.isNew;
                b.supplierID = book.supplierID;
            }
            datacontext.SubmitChanges();
        }

        public IEnumerable<Books> GetBooksByString(string searched)
        {
            var searchResult = from b in datacontext.Books
                               where b.b_name.Contains(searched)
                               || b.b_author.Contains(searched)
                               select b;

            return searchResult;
        }
    }
}
