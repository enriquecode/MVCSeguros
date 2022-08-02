using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSeguros.Models
{
    public class Cotizacion
    {
        public string Descripcion { get; set; }
        public string DescripcionId { get; set; }
        public bool PeticionFinalizada { get; set; }
        public int PeticionId { get; set; }
        public string PeticionLlave { get; set; }

        public List<PeticionAseguradora> Aseguradoras { get; set; }
    }
}
