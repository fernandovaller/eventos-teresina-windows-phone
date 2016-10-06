using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploServices
{
    class EventosDataContext : DataContext
    {
        //Chama o construtor da classe base, super classe
        public EventosDataContext() : base("isostore:/eventos.sdf") { }

        public Table<Eventos> Eventos;
    }
}
