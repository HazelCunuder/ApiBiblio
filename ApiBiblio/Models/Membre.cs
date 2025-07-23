namespace ApiBiblio.Models
{
    public class Membre
    {
        public int Id { get; set; }
        public string? Nom_Membre{ get; set; }
        public string? Prenom_Membre { get; set; }
        public string? Mdp_Membre { get; set; }
        public string? Adresse_postale {  get; set; }
        public string? Adresse_mail { get; set; }
        public string? Telephone {  get; set; }
        public string? Historique_Membre { get; set; }
    }
}
