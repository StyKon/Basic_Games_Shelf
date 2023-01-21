using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Basic_Games_Shelf.DOMAINE;

namespace Basic_Games_Shelf.WebApi.Data
{
    public class BasicGamesShelfContext : DbContext
    {
        public BasicGamesShelfContext (DbContextOptions<BasicGamesShelfContext> options)
            : base(options)
        {
        }
        public DbSet<Basic_Games_Shelf.DOMAINE.Games> Games { get; set; } = default!;
    }
}
