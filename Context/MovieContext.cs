using A13MovieLibrary.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace A13MovieLibrary.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Genre> Genres {get; set;}
        public DbSet<Movie> Movies {get; set;}
        public DbSet<MovieGenre> MovieGenres {get; set;}
        public DbSet<Occupation> Occupations {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<UserMovie> UserMovies {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=bitsql.wctc.edu;Database=lhernandez19_22097_A13MovieLibrary;User ID=lhernandez19;Password=000557680"
            );
        }
    }
}
