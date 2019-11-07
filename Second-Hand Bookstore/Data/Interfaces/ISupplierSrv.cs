using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModels;

namespace Logic.Interfaces
{
    public interface ISupplierSrv
    {
        void CreateSupplier(tSupplier supplier);
        tSupplier GetSupplier(int id);
        void UpdateSupplier(tSupplier supplier);
        void DeleteSupplier(int id);
        List<tSupplier> GetSupplierList();
    }
}
