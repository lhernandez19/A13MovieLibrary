using System;
using A13MovieLibrary.Data;

namespace A13MovieLibrary.Menus
{
    public class Menu : IMenu
    {
        private IContext _context;

        public Menu(IContext context)
        {
            _context = context;
        }

        public void DisplayMenu()
        {
            string menuSelection;
            var repo = new Repository();

            do
            {
                ActionMenu();
                menuSelection = Console.ReadLine();

                switch (menuSelection)
                {
                    case "1": // Add movie
                    _context.AddOneMovie();
                    break;
                    case "2": // Edit movie
                    _context.EditOneMovie();
                    break;
                    case "3": // Search all movies
                    _context.SearchAllMovies();
                    break;
                    case "4": // Serach specfic movie
                    _context.SearchOneMovie();
                    break;
                    case "5": // Add User
                    _context.AddOneUser();
                    break;
                    case "6": // Delete movie
                    _context.DeleteOneMovie();
                    break;
                }
            } while (menuSelection != "7");
        }
        
        public void ActionMenu()
        {
            Console.WriteLine("\nMake a selection\n");
            Console.WriteLine("1. Add new records to the Movie table - (Create)");
            Console.WriteLine("2. Edit a specified record from the Movie table - (Update)");
            Console.WriteLine("3. Display all records in the Movie table - user decides if they want to see all - (Search)");
            Console.WriteLine("4. Display a specific Movie - (Search)");
            Console.WriteLine("5. Add new user - including occupation - (Create)");
            Console.WriteLine("6. Delete Movie - (Delete)");
            Console.WriteLine("7. Exit\n");
        }

    }
}

