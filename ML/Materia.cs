using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public byte? Creditos { get; set; }
        public decimal? Costo { get; set; }
        public ML.Semestre Semestre { get; set; }
        public List<object> Materias { get; set; }
        public ML.Grupo Grupo { get; set; }
        public ML.Plantel Plantel { get; set; }
        public string? Imagen { get; set; }
        public bool Status { get; set; }
    }
}
