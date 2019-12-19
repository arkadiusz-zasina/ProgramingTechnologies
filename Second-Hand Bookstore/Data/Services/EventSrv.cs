using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data.Services
{
    public class EventSrv : IEventSrv
    {
        private IDBContextDataContext datacontext;

        public EventSrv(IDBContextDataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public float GetAccountBalance()
        {
            return (float)datacontext.Events.OrderByDescending(x => x.id).FirstOrDefault().account_balance.Value;
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
