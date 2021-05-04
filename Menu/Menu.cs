using System;
using System.Linq;
using A13MovieLibrary.Context;
using A13MovieLibrary.DataModels;

namespace A13MovieLibrary.Menus
{
    public class Menu
    {
        public void DisplayMenu()
        {
            string menuSelection;

            do
            {
                ActionMenu();
                menuSelection = Console.ReadLine();

                switch (menuSelection)
                {
                    case "1": // Search movie
                    Search();
                    break;
                    case "2": // Add movie
                    AddUser();
                    break;
                    case "3": // Update movie
                    Update();
                    break;
                    case "4": // Delete movie
                    Delete();
                    break;
                }
            } while (menuSelection != "5");
        }
        
        public void Search()
        {
            System.Console.WriteLine("Do you want to display \n1. All movies\n2. Specific movie\n");
            string choice = Console.ReadLine();
            if(choice == "1"){
                using(var db = new MovieContext())
                    {
                        var movies = db.Movies;
                        foreach (var movie in movies)
                        {
                            Console.WriteLine($"Movie found: Id - {movie.Id, -5} | Title - {movie.Title, -10} | Release Date - {movie.ReleaseDate, -15}");
                        }
                    }
            } else if (choice == "2"){
                Console.Write("\nSearch by movie title: ");
                string mTitle = Console.ReadLine().ToLower();

                using(var db = new MovieContext())
                {
                    var foundMovie = db.Movies.Where(m => m.Title.Contains(mTitle));

                    if(foundMovie.Count() < 1)
                    {
                        Console.WriteLine("\nRecord not found");
                    }

                    foreach (var movie in foundMovie)
                    {
                        Console.WriteLine($"Movie found: Id - {movie.Id, -5} | Title - {movie.Title, -10} | Release Date - {movie.ReleaseDate, -15}");
                    }
                }
            }
        }

        public void Add()
        {
            Console.Write("\nSearch by movie title: ");
            string mTitle = Console.ReadLine();

            Console.Write("Enter new release date (mm/dd/yyyy): ");
            var mReleseDate = System.Console.ReadLine();
           
            using(var db = new MovieContext())
            {
                var movie = new Movie()
                {
                    Title = mTitle,
                    ReleaseDate = Convert.ToDateTime(mReleseDate)
                };    

                Console.WriteLine($"Movie added: Title - {movie.Title, -10} | Release Date - {movie.ReleaseDate, -20}");            

                db.Movies.Add(movie);
                db.SaveChanges();
            }
        }

        public void Update()
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

                Console.WriteLine($"Movie to update: Id - {foundMovie.Id, -5} | Title - {foundMovie.Title, -10} | Release Date - {foundMovie.ReleaseDate, -15}");

                db.Movies.Update(foundMovie);
                db.SaveChanges(); 
            }
        }

        public void Delete()
        {
            Console.Write("\nDelete movie title: ");
            string mTitle = Console.ReadLine();            

            using (var db = new MovieContext())
            {
                var foundMovie = db.Movies.FirstOrDefault(m => m.Title == mTitle);
                
                Console.WriteLine($"Movie to remove: Id - {foundMovie.Id, -5} | Title - {foundMovie.Title, -10} | Release Date - {foundMovie.ReleaseDate, -15}");
               
                db.Movies.Remove(foundMovie);
                db.SaveChanges();
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

                Console.WriteLine($"User added: Age - {user.Age} | Gender - {user.Gender} | ZipCode - {user.ZipCode}");            

                db.Users.Add(user);
                db.SaveChanges();
            }
        }


        public void ActionMenu()
        {
            Console.WriteLine("\nMake a selection\n");
            Console.WriteLine("1. Search movie");
            Console.WriteLine("2. Add movie");
            Console.WriteLine("3. Update movie");
            Console.WriteLine("4. Delete movie");
            Console.WriteLine("5. Exit\n");
        }
    }
}

