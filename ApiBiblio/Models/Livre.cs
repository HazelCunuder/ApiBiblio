using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBiblio.Models
{
    public class Livre
    {
        public int Id { get; set; }

        [DisplayName("Auteur")]
        [Required(ErrorMessage = "Veuillez renseigner l'auteur du livre'")]
        public int IdAuteur { get; set; }

        [ForeignKey("IdAuteur")]
        public Auteur? Auteur { get; set; }

        [DisplayName("Titre")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Veuillez insérez un titre")]
        public required string Titre { get; set; }

        [DisplayName("Est-ce que le livre est Disponible ?")]
        public bool Disponible { get; set; } = true;

        [DisplayName("Année de Publication")]
        public Int16 AnnePublication { get; set; }

        [DisplayName("ISBN")]
        [StringLength(17, MinimumLength = 13)]
        [RegularExpression(@"^(?:ISBN(?:-13)?:?\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)97[89][-\ ]?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$", ErrorMessage = "Veuillez entrer un ISBN valide")]
        public string? ISBN { get; set; }

        [DisplayName("Catégorie de l'oeuvre")]
        [Required(ErrorMessage = "Veuillez renseigner la catégorie du livre")]
        public int IdCategorie { get; set; }

        [ForeignKey("IdCategorie")]
        public Categorie? Categorie { get; set; }

        [DisplayName("Genre de l'oeuvre")]
        [Required(ErrorMessage = "Veuillez renseigner le genre du livre")]
        public int IdGenre { get; set; }

        [ForeignKey("IdGenre")]
        public Genre? Genre { get; set; }

        public ICollection<Livre_Auteur> Livre_Auteurs { get; set; } = new List<Livre_Auteur>();

        public ICollection<Livre_Genre> Livre_Genres { get; set; } = new List<Livre_Genre>();
    }
}
