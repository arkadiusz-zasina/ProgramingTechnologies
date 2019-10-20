using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModels;

namespace Logic.Interfaces
{
    public interface IClientSrv
    {
        void CreateClient(tClient client);
        tClient GetClient(int id);
        void UpdateClient(tClient client);
        void DeleteClient(int id);
        IEnumerable<tClient> GetClientList();

    }
}