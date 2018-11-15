using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana.Models
{
    public class ChambreVM
    {
        public short Numero { get; set; }
        public byte Etage { get; set; }
        public bool Bain { get; set; }
        public bool? Douche { get; set; }
        public bool? Wc { get; set; }
        public byte NbLits { get; set; }
        public short? NumTel { get; set; }

        public decimal Prix { get; set; }
    }
}
