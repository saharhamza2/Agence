using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.ApplicationCore.Domaine
{
    public class Reservation
    {
        [DataType(DataType.Date)]
        public DateTime DateReservation { get; set; }
        [Range(1,4)]
        public int NbPersonnes { get; set; }
        public int PackId { get; set; }
        public int Identifiant { get; set; }

        [ForeignKey("Identifiant")]
        public virtual Client Client { get; set; }

        [ForeignKey("PackId")]
        public virtual Pack Pack { get; set; }
    }
}
