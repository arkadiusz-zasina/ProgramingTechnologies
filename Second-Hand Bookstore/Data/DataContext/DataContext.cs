using Data.DataModels;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContext
{
    public class DataContext
    {
        public List<tBook> Books { get; set; }
        public List<tClient> Clients { get; set; }
        public List<tSupplier> Suppliers { get; set; }
        public List<tEvent> Events { get; set; }

        private IFiller Filler;

        //fill method
        public DataContext(IFiller filler) //interface //pass value bool (1)
        { 
            // if (1) nie można utworzyć typów, bo są nieznane, bo tworzyłyby odniesienia do warstwy wyżej (referencja)
            Books = new List<tBook>();
            Clients = new List<tClient>();
            Suppliers = new List<tSupplier>();
            Events = new List<tEvent>();
            this.Filler = filler;
            Fill();
        }

        public void Fill()
        {
            Books.AddRange(Filler.Fill().Books);
            Clients.AddRange(Filler.Fill().Clients);
            Suppliers.AddRange(Filler.Fill().Suppliers);
            Events.AddRange(Filler.Fill().Events);
        }

        public void SetFiller(IFiller filler)
        {
            this.Filler = filler;
        }
    }
}
