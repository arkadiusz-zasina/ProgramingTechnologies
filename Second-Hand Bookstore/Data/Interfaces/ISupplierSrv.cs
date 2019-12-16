using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModels;

namespace Data.Interfaces
{
    public interface ISupplierSrv
    {
        void CreateSupplier(Suppliers supplier);
        Suppliers GetSupplier(int id);
        void UpdateSupplier(Suppliers supplier);
        void DeleteSupplier(int id);
        List<Suppliers> GetSupplierList();
        Boolean isSupplierAvailable(int id);
    }
}
