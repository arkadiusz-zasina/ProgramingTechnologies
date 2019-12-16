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
    public class ClientSrv : IClientSrv
    {
        private DBContextDataContext datacontext;

        public ClientSrv(DBContextDataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public void CreateClient(Clients client)
        {
            datacontext.Clients.InsertOnSubmit(client);
            datacontext.SubmitChanges();
        }

        public void DeleteClient(int id)
        {
            datacontext.Clients.DeleteOnSubmit(datacontext.Clients.Where(i => i.id == id).Single());
            datacontext.SubmitChanges();
        }

        public Clients GetClient(int id)
        {
            return datacontext.Clients.Single(x => x.id == id);
        }

        public List<Clients> GetClientList()
        {
            return datacontext.Clients.ToList();
        }

        public void UpdateClient(Clients client)
        {
            DeleteClient(client.id);
            CreateClient(client);
        }
    }
}
