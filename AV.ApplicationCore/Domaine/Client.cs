using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.ApplicationCore.Domaine
{
    public class Client
    {
        [Key]
        public int Identifiant { get; set; }
        [Required(ErrorMessage ="Login est obligatoire")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password est obligatoire")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Photo { get; set; }
        public string Telephone { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public int ConseillerFK { get; set; }
        [ForeignKey("ConseillerFK")]
        public virtual Conseiller Conseiller { get; set; }
    }
}
