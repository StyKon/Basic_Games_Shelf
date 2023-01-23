
using Basic_Games_Shelf.DOMAINE.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Basic_Games_Shelf.DOMAINE
{
    public class Games
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Game { get; set; }
        [Required]
        public int PlayTime { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        [MinLength(1)]
        [StringEnumerableItemMinLengthAttribute(1)]
        public string[] Platforms { get; set; }

    }
}