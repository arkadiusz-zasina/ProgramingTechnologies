using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IEventSrv
    {
        void RegisterEvent(Events _event);
        List<Events> GetListOfEvents();
        float GetAccountBalance();
        int GetLastId();
    }
}
