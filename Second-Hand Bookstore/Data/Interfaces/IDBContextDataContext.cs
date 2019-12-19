using System.Data.Linq;

namespace Data
{
    public interface IDBContextDataContext
    {
        Table<Books> Books { get; }
        Table<Clients> Clients { get; }
        Table<Events> Events { get; }
        Table<Suppliers> Suppliers { get; }
        void SubmitChanges();
    }
}