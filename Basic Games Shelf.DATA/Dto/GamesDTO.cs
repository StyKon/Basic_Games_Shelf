
using System.ComponentModel.DataAnnotations;

namespace Basic_Games_Shelf.DATA.Dto
{
    public class GamesDTO
    {
        public int UserId { get; set; }
        public string Game { get; set; }
        public int PlayTime { get; set; }
        public string Genre { get; set; }
        public string[] Platforms { get; set; }

    }
}