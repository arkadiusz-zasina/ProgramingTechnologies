using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataModels;

namespace Logic.Interfaces
{
    public interface IEventSrv
    {
        void RegisterEvent(tEvent _event);
        List<tEvent> GetListOfEvents();

        float GetAccountBalance();
        int GetLastId();
    }
}
