using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExemploServices
{
    [DataContract]
    class ResultadoEventos
    {
        [DataMember(Name = "nome")]
        public string Nome { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        [DataMember(Name = "destaque")]
        public string Destaque { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }


    }
}
