using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VentaMateria
    {
        public int IdVentaMateria{ get; set; } 
        public ML.Venta Venta { get; set; }
        public int Cantidad { get; set; }
        public ML.Materia Materia { get; set; }
        public List<object> VentaMaterias { get; set; }
        public decimal Total { get; set; }
    }
}
