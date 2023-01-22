using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Games_Shelf.DATA.Response
{
    public class GamesResponse
    {
        public string Game { get; set; }
        public string Genre { get; set; }
        public string[] Platforms { get; set; }
        public int TotalPlayTime { get; set; }
        public int TotalPlayers { get; set; }
    }
}
