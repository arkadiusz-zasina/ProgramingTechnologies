using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Client
    {
        public int id { get; set; }
        public String c_name { get; set; }
        public String c_surname { get; set; }
        public DateTime creationDate{ get; set; }
    }
}
