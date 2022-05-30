using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Materium
    {
        public Materium()
        {
            Direccions = new HashSet<Direccion>();
        }

        public int IdMateria { get; set; }
        public string? Nombre { get; set; }
        public byte? Creditos { get; set; }
        public decimal Costo { get; set; }
        public int? IdSemestre { get; set; }
        public string? Imagen { get; set; }
        public bool Status { get; set; }

        //propiedades foraneas
        public int? IdGrupo { get; set; }
        public string Horario { get; set; }
        public int IdPlantel { get; set; }
        public string Plantel { get; set; }
        public virtual Semestre? IdSemestreNavigation { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }
    }
}
