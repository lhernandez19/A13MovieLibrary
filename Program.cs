using System;
using A13MovieLibrary.Menus;

namespace A13MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var menu = new Menu();
            menu.DisplayMenu();

            Console.WriteLine("Thanks for using the Movie Library!");

        }
    }
}
