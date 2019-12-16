using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContext;
using Data.DataModels;
using Data.Interfaces;

namespace Data.Services
{
    public class SupplierSrv : ISupplierSrv
    {
        private DBContextDataContext datacontext;

        public SupplierSrv(DBContextDataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public void CreateSupplier(Suppliers supplier)
        {
            datacontext.Suppliers.InsertOnSubmit(supplier);
            datacontext.SubmitChanges();
        }

        public void DeleteSupplier(int id)
        {
            datacontext.Suppliers.DeleteOnSubmit(datacontext.Suppliers.Where(i => i.id == id).Single());
            datacontext.SubmitChanges();
        }

        public Suppliers GetSupplier(int id)
        {
            return datacontext.Suppliers.Single(x => x.id == id);
        }

        public List<Suppliers> GetSupplierList()
        {
            return datacontext.Suppliers.ToList();
        }

        public void UpdateSupplier(Suppliers supplier)
        {
            var toupdate = from s in datacontext.Suppliers
                           where s.id == s.id
                           select s;
        }
    }
}
