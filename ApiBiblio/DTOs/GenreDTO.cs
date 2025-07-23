using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public required string NomGenre { get; set; }

        public GenreDTO() { }

        public GenreDTO(Genre genre) =>
            (Id, NomGenre) = (genre.Id, genre.NomGenre);
    }
}
