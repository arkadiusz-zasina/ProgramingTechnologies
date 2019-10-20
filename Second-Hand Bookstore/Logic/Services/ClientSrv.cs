using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModels;
using Logic.Interfaces;

namespace Logic.Services
{
    class ClientSrv : IClientSrv
    {
        public void CreateClient(tClient client)
        {
            Database.Books.Add(client);
        }

        public void DeleteClient(int id)
        {
            Database.Books.RemoveAll(x => x.Id == id);
        }

        public tClient GetClient(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tClient> GetClientList()
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(tClient client)
        {
            throw new NotImplementedException();
        }
    }
}
