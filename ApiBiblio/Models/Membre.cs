namespace ApiBiblio.Models
{
    public class Membre
    {
        public int Id { get; set; }
        public string? nom_Membre{ get; set; }
        public string? prenom_Membre { get; set; }
        public string? mdp_Membre { get; set; }
        public string? adresse_postale {  get; set; }
        public string? adresse_mail { get; set; }
        public string? telephone {  get; set; }
        public string? historique_Membre { get; set; }
    }
}
