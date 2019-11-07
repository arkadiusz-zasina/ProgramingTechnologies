using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class tBookBoughtEvent : tEvent
    {
        public tSupplier Supplier { get; set; }
    }
}
