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
    public class EventSrv : IEventSrv
    {
        Database database;

        public EventSrv(Database database)
        {
            this.database = database;
        }

        public float GetAccountBalance()
        {
            return database.Events.Last().AccountBalance;
        }

        public int GetLastId()
        {
            return database.Events.Last().Id;
        }

        public List<tEvent> GetListOfEvents()
        {
            return database.Events;
        }

        public void RegisterEvent(tEvent _event)
        {
            database.Events.Add(_event);
        }
    }
}
