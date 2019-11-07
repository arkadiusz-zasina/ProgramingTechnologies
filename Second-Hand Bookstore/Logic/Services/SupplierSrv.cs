﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContext;
using Data.DataModels;
using Logic.Interfaces;

namespace Logic.Services
{
    public class SupplierSrv : ISupplierSrv
    {
        private DataContext datacontext;

        public SupplierSrv(DataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public void CreateSupplier(tSupplier supplier)
        {
            datacontext.Suppliers.Add(supplier);
        }

        public void DeleteSupplier(int id)
        {
            datacontext.Suppliers.RemoveAll(x => x.Id == id);
        }

        public tSupplier GetSupplier(int id)
        {
            return datacontext.Suppliers.Single(x => x.Id == id);
        }

        public List<tSupplier> GetSupplierList()
        {
            return datacontext.Suppliers;
        }

        public void UpdateSupplier(tSupplier supplier)
        {
            var tempsupplier = datacontext.Suppliers.Single(x => x.Id == supplier.Id);
            tempsupplier = supplier;
        }
    }
}
