using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.ApplicationCore.Domaine
{
    public class Activite
    {
        [Key]
        public int ActiviteId { get; set; }

        public string Ville { get; set; }
        public string Pays { get; set; }
        public double Prix { get; set; }
        public string TypeService { get; set; }
        public ICollection<Pack> Packs { get; set; }
    }
}
