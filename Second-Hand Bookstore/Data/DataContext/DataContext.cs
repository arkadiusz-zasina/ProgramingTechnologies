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

        public IFiller Filler {
            get { return Filler; } 
            set { 
                Filler = value; 
                Fill(); 
            } 
        }

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
            Books = Filler.Fill().Books;
            Clients = Filler.Fill().Clients;
            Suppliers = Filler.Fill().Suppliers;
            Events = Filler.Fill().Events;
        }
    }
}
