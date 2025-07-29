using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Employe_Emprunt
    {
        public int EmployeId { get; set; }
        public int EmpruntId { get; set; }

        public Emprunt? Emprunt { get; set; }
        public Employe? Employe { get; set; }
    }
}
