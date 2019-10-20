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
    class ClientSrv : IClientSrv
    {
        Database database;

        public ClientSrv(Database database)
        {
            this.database = database;
        }

        public void CreateClient(tClient client)
        {
            database.Clients.Add(client);
        }

        public void DeleteClient(int id)
        {
            database.Clients.RemoveAll(x => x.Id == id);
        }

        public tClient GetClient(int id)
        {
            return database.Clients.Single(x => x.Id == id);
        }

        public IEnumerable<tClient> GetClientList()
        {
            return database.Clients;
        }

        public void UpdateClient(tClient client)
        {
            var tempclient = database.Clients.Single(x => x.Id == client.Id) ;
            tempclient = client;
        }
    }
}
