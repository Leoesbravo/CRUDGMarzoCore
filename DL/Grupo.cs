using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Grupo
    {
        public int IdGrupo { get; set; }
        public string? Horario { get; set; }
        public int? IdPlantel { get; set; }
        public int? IdMateria { get; set; }

        public virtual Materium? IdMateriaNavigation { get; set; }
        public virtual Plantel? IdPlantelNavigation { get; set; }
    }
}
