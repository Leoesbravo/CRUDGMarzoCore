using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Pai
    {
        public Pai()
        {
            Estados = new HashSet<Estado>();
        }

        public int IdPais { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Estado> Estados { get; set; }
    }
}
