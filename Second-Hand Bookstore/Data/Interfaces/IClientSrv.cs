using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModels;

namespace Data.Interfaces
{
    public interface IClientSrv
    {
        void CreateClient(Clients client);
        Clients GetClient(int id);
        void UpdateClient(Clients client);
        void DeleteClient(int id);
        List<Clients> GetClientList();
        IEnumerable<Clients> GetClientsByString(string search);
    }
}