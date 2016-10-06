using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploServices
{
    [Table]
    public class Eventos
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Nome { get; set; }

        [Column]
        public string Data { get; set; }

        [Column]
        public string Destaque { get; set; }

        [Column]
        public string Image { get; set; }
    }
}
