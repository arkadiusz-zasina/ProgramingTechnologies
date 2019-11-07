using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContext;
using Data.DataModels;
using Logic.Interfaces;

namespace Logic.Services
{
    public class ClientSrv : IClientSrv
    {
        private DataContext datacontext;

        public ClientSrv(DataContext database)
        {
            this.datacontext = database;
        }

        public void CreateClient(tClient client)
        {
            datacontext.Clients.Add(client);
        }

        public void DeleteClient(int id)
        {
            datacontext.Clients.RemoveAll(x => x.Id == id);
        }

        public tClient GetClient(int id)
        {
            return datacontext.Clients.Single(x => x.Id == id);
        }

        public List<tClient> GetClientList()
        {
            return datacontext.Clients;
        }

        public void UpdateClient(tClient client)
        {
            var tempclient = datacontext.Clients.Single(x => x.Id == client.Id) ;
            tempclient = client;
        }
    }
}
