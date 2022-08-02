using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSeguros.Models
{
    public class PeticionAseguradora
    {
        public int AseguradoraId { get; set; }
        public int PeticionAseguradoraId { get; set; }
        public int PeticionId { get; set; }        
        public decimal Tarifa { get; set; }

    }
}
