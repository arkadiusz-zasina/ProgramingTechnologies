using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Database;
using Data.DataModels;
using Logic.Interfaces;

namespace Logic.Services
{
    class SupplierSrv : ISupplierSrv
    {
        Database database;

        public SupplierSrv(Database database)
        {
            this.database = database;
        }

        public void CreateSupplier(tSupplier supplier)
        {
            database.Suppliers.Add(supplier);
        }

        public void DeleteSupplier(int id)
        {
            database.Suppliers.RemoveAll(x => x.Id == id);
        }

        public tSupplier GetSupplier(int id)
        {
            return database.Suppliers.Single(x => x.Id == id);
        }

        public IEnumerable<tSupplier> GetSupplierList()
        {
            return database.Suppliers;
        }

        public void UpdateSupplier(tSupplier supplier)
        {
            var tempsupplier = database.Suppliers.Single(x => x.Id == supplier.Id);
            tempsupplier = supplier;
        }
    }
}
