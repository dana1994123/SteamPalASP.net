using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Game
    {
        public String name { get; set; }
        public long SteamId { get; set; }
        public long IgdbId { get; set; }
        public String ImageUrl { get; set; }
        public List<String> Genres { get; set; }
        public List<String> Platforms { get; set; }
        public List<String> Companies { get; set; }
        public long ReleaseDate { get; set; }
    }
}