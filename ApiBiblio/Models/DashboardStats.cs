using System.Collections.Generic;

namespace ApiBiblio.Models
{
    public class DashboardStats
    {
        public int LivreCount { get; set; }
        public int LivreDispoCount { get; set; }
        public int EmpruntEnCoursCount { get; set; }
        public int MembreCount { get; set; }
        public List<Emprunt> EmpruntsRecents { get; set; } = new();
    }
}
