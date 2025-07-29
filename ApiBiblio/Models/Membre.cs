using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Membre
    {
        public int Id { get; set; }

        [DisplayName("Nom")]
        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$", ErrorMessage = "Veuillez entrer un nom valide")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        [StringLength(150)]
        public required string NomMembre{ get; set; }

        [DisplayName("Prénom")]
        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$", ErrorMessage = "Veuillez entrer un prénom valide")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        [StringLength(150)]
        public required string PrenomMembre { get; set; }

        [DisplayName("Mot de Passe")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$", ErrorMessage = "Le mot de passe doit contenir un chiffre, une lettre en Majuscule, une lettre en mininuscule et un caractère spécial")]
        [StringLength(16, ErrorMessage = "Le mot de passe doit contenir entre 8 et 16 caractères.", MinimumLength = 8)]
        public required string MdpMembre { get; set; }

        [DisplayName("Adresse")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        public required string AdressePostale {  get; set; }

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse email valide")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        public  required string AdresseMail { get; set; }

        [DisplayName("Téléphone")]
        [RegularExpression(@"^(0[1-9])(?:[ .-/]?[0-9]{2}){4}$", ErrorMessage = "Veuillez entrer un numéro de téléphone français valide (ex: 06 12 34 56 78)")]
        [Phone]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        public required string Telephone {  get; set; }
        public List<string>? HistoriqueMembre { get; set; } = new();
    }
}
