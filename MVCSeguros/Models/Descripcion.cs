using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSeguros.Models
{
    public class Descripcion
    {
        public string DescripcionId { get; set; }
        public int ModeloId { get; set; }
        public string DescripcionDetallada { get; set; }
    }
}
