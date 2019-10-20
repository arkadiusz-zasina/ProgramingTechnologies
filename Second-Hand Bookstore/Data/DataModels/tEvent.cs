using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class tEvent
    {
        public int Id { get; set; }
        public float AccountBalance { get; set; }
        public DateTime EventTime { get; set; }
        public int BookId { get; set; }
    }
}
