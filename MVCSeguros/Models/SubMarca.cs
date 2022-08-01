using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSeguros.Models
{
    public class SubMarca
    {
        public int SubMarcaId { get; set; }
        public int MarcaId { get; set; }
        public string SubMarcaDescripcion { get; set; }
    }
}
