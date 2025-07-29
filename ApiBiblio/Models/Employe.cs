using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.InteropServices;

namespace ApiBiblio.Models
{
    public class Employe
    {
        public int Id { get; set; }

        [DisplayName("Nom")]
        [RegularExpression(@"[A-Z]+[A-zà-ÿ\s'-]+$", ErrorMessage = "Veuillez entrer un nom valide")]
        [StringLength(200)]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        public required string NomEmploye { get; set; }

        [DisplayName("Prénom")]
        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$", ErrorMessage = "Veuillez entrer un prénom valide")]
        [StringLength(200)]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        public required string PrenomEmploye { get; set; }

        [DisplayName("Nom d'utilisateur")]
        [Required(ErrorMessage = "Vous devez rentrer un nom d'utilisateur")]
        public required string LoginEmploye { get; set; } // Identifiant utilisé pour se connecter

        [DisplayName("Mot de Passe")]
        [Required(ErrorMessage = " Vous ne pouvez pas laisser ce champ vide")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$", ErrorMessage = "Le mot de passe doit contenir un chiffre, une lettre en Majuscule, une lettre en mininuscule et un caractère spécial")]
        [StringLength(16, ErrorMessage = "Le mot de passe doit contenir entre 8 et 16 caractères.", MinimumLength = 8)]
        public required string MdpEmploye { get; set; }

        [Required]
        public int IdRole { get; set; }

        [ForeignKey("IdRole")]
        public Role? Role { get; set; }
    }
}
