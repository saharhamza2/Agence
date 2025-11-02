using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.ApplicationCore.Domaine
{
    public class Pack
    {
        public int PackId { get; set; }
        public int NbPlaces { get; set; }
        public int Duree { get; set; }
        public string IntitulePack { get; set; }
        public DateTime DateDebut { get; set; }
        public ICollection<Activite> Activites { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
