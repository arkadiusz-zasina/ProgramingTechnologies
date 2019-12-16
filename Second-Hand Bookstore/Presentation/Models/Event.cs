using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Event
    {
        public int id { get; set; }
        public float account_balance { get; set; }
        public DateTime event_time { get; set; }
        public int bookId { get; set; }
        public int clientId { get; set; }
        public int supplierId { get; set; }
    }
}
