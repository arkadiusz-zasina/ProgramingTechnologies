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
    public class EventSrv : IEventSrv
    {
        private DataContext datacontext;

        public EventSrv(DataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public float GetAccountBalance()
        {
            return datacontext.Events.Last().AccountBalance;
        }

        public int GetLastId()
        {
            return datacontext.Events.Last().Id;
        }

        public List<tEvent> GetListOfEvents()
        {
            return datacontext.Events;
        }

        public void RegisterEvent(tEvent _event)
        {
            datacontext.Events.Add(_event);
        }
    }
}
