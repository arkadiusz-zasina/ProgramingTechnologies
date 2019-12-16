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

        public IEnumerable<Clients> GetClientsByString(string search)
        {
            var searchResult = from c in datacontext.Clients
                               where c.c_name.Contains(search)
                               || c.c_surname.Contains(search)
                               select c;

            return searchResult;
        }

        public void UpdateClient(Clients client)
        {
            var toupdate = from c in datacontext.Clients
                           where c.id == client.id
                           select c;

            foreach(Clients c in datacontext.Clients)
            {
                c.c_name = client.c_name;
                c.c_surname = client.c_surname;
            }
        }
    }
}
