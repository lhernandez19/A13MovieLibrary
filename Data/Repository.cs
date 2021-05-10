using System;
using System.Collections.Generic;
using System.Linq;
using A13MovieLibrary.Context;
using A13MovieLibrary.DataModels;
using Microsoft.EntityFrameworkCore;

namespace A13MovieLibrary.Data
{
    public class Repository : IRepository
    {

        public void SearchAll()
        {
            System.Console.WriteLine("\nDisplaying all movies: ");

            using(var db = new MovieContext())
                {
                    var movies = db.Movies;
                    foreach (var movie in movies)
                    {
                        Console.WriteLine($"Movie found: Id - {movie.Id, -5} | Title - {movie.Title, -10} | Release Date - {movie.ReleaseDate, -15}");
                    }
                }
        }

        public void SearchOne()
        {
            Console.Write("\nSearch by movie title: ");
            string mTitle = Console.ReadLine().ToLower();

            using(var db = new MovieContext())
            {
                var foundMovie = db.Movies.Where(m => m.Title.Contains(mTitle));
                
                do
                {
                    if(foundMovie.Count() < 1)
                    {
                        Console.WriteLine("\nRecord not found");
                    
                        Console.Write("\nSearch by movie title: ");
                        mTitle = Console.ReadLine().ToLower();
                    }

                } while (foundMovie.Count() == 0);

                foreach (var movie in foundMovie)
                {
                    Console.WriteLine($"\n Movie found: Id - {movie.Id, -5} | Title - {movie.Title, -10} | Release Date - {movie.ReleaseDate, -15}");
                }
            }
        }

        public void AddMovie()
        {
            Console.Write("\nEnter movie title: ");
            string mTitle = Console.ReadLine();

            Console.Write("\nEnter new release date (mm/dd/yyyy): ");
            var mReleseDate = System.Console.ReadLine();

            using(var db = new MovieContext())
            {
                var movie = new Movie()
                {
                    Title = mTitle,
                    ReleaseDate = Convert.ToDateTime(mReleseDate)
                };    

                Console.WriteLine($"\n Movie added: Title - {movie.Title, -10} | Release Date - {movie.ReleaseDate, -20}");            

                db.Movies.Add(movie);
                db.SaveChanges();
            }
        }

        public void EditMovie()
        {
            Console.Write("\nSearch movie title: ");
            string mTitle = Console.ReadLine();

            Console.Write("\nUpdate movie title: ");
            string newTitle = Console.ReadLine();

            Console.Write("\nUpdate movie title (mm/dd/yyyy): ");
            var newReleaseDate = Console.ReadLine();

            using(var db = new MovieContext())
            {
                var foundMovie = db.Movies.Where(m => m.Title.Contains(mTitle))
                .FirstOrDefault();

                foundMovie.Title = newTitle;
                foundMovie.ReleaseDate = Convert.ToDateTime(newReleaseDate);

                Console.WriteLine($"\n Movie to update: Id - {foundMovie.Id, -5} | Title - {foundMovie.Title, -10} | Release Date - {foundMovie.ReleaseDate, -15}");

                db.Movies.Update(foundMovie);
                db.SaveChanges(); 
            }
        }

        public void DeleteMovie()
        {
            using (var db = new MovieContext())
            {
                string mTitle;
                string deleteMovie;
                var foundMovie = db.Movies.Where(m => m.Title.Contains(""))
                    .FirstOrDefault();
                do
                {
                    Console.Write("\nDelete movie title: ");
                    mTitle = Console.ReadLine();  

                    foundMovie = db.Movies.Where(m => m.Title.Contains(mTitle))
                    .FirstOrDefault();

                    Console.WriteLine($"\nDo you want to delete this movie (y/n)? Id - {foundMovie.Id, -5} | Title - {foundMovie.Title, -10} | Release Date - {foundMovie.ReleaseDate, -15}");
                    deleteMovie = Console.ReadLine().ToLower();

                } while (deleteMovie == "n");

                    db.Movies.Remove(foundMovie);
                    db.SaveChanges(); 

                    Console.WriteLine($"\n The movie {foundMovie.Title} was deleted!");
            }  
        }

        public void AddUser()
        {            
            Console.Write("Enter user age: ");
            var uAge = Int32.Parse(Console.ReadLine());

            Console.Write("\nEnter user gender: ");
            var uGender = Console.ReadLine();

            Console.Write("\nEnter user zipcode: ");
            var uZipCode = Console.ReadLine();

            Console.Write("\nEnter user occupation: ");
            var uOccupation = Console.ReadLine();

            using(var db = new MovieContext())
            {
                var foundOccupation = db.Occupations.Where(occ => occ.Name.Contains(uOccupation))
                .FirstOrDefault();
                if(foundOccupation == null)
                {
                    foundOccupation = db.Occupations.Where(occ => occ.Name == ("Other")).FirstOrDefault();
                }

                var user = new User()
                {
                    Age = uAge,
                    Gender = uGender,
                    ZipCode = uZipCode,
                    Occupation = foundOccupation
                };    

                db.Users.Add(user);
                db.SaveChanges();

                var lastId = db.Users.OrderBy(u => u.Id).Last();
                int newUserId = (int)lastId.Id;       

                var userDetails = db.Users.Include(ud => ud.Occupation).Where(ud => ud.Id == newUserId).FirstOrDefault();

                Console.WriteLine($"\n New user details: Id - {userDetails.Id} | Gender - {userDetails.Gender} | ZipCode - {userDetails.ZipCode} | Occupation - {userDetails.Occupation.Name}");
            }
        }

        public void RatingMovie()
        {
            using (var db = new MovieContext()) 
            { 
                Console.Write("\nEnter the user Id that will rate the movie: ");
                var userId = Int64.Parse(Console.ReadLine());

                var mTitle = "";

                var userFound = db.Users.Where(u => u.Id == userId); 
                
                do
                {
                    if(userFound.Count() < 1)
                    {
                        Console.WriteLine("\nRecord not found");
                    
                        Console.Write("\nEnter the user Id that will rate the movie: ");
                        userId = Int64.Parse(Console.ReadLine());
                    }

                } while (userFound.Count() == 0);

                var userFound2 = db.Users.Where(u => u.Id == userId).FirstOrDefault();

                Console.Write("\nEnter the movie that will be rated:  ");
                mTitle = Console.ReadLine();

                var movieFound = db.Movies.FirstOrDefault(m=>m.Title == mTitle );

                Console.Write("\nHow do you rate the movie?  ");
                var mRate = Int64.Parse(Console.ReadLine());

                var userMovie = new UserMovie()
                {
                    Rating = mRate,
                    RatedAt = DateTime.Now,
                    User = userFound2,
                    Movie = movieFound
                };

                var userMovies = new List<UserMovie>();
                userMovies.Add(userMovie);
                
                db.UserMovies.Add(userMovie);

                db.SaveChanges();
                
                Console.WriteLine($"\n User Id: {userMovie.User.Id} | Movie title: {userMovie.Movie.Title} | Movie rating: {userMovie.Rating}");

            }
        }
    }
}