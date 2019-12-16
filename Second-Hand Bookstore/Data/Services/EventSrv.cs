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
    public class EventSrv : IEventSrv
    {
        private DBContextDataContext datacontext;

        public EventSrv(DBContextDataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public float GetAccountBalance()
        {
            return (float)datacontext.Events.OrderByDescending(x => x.id).First().account_balance;
        }

        public int GetLastId()
        {
            return datacontext.Events.OrderByDescending(x => x.id).First().id;
        }

        public List<Events> GetListOfEvents()
        {
            return datacontext.Events.ToList();
        }

        public void RegisterEvent(Events _event)
        {
            datacontext.Events.InsertOnSubmit(_event);
            datacontext.SubmitChanges();
        }
    }
}
