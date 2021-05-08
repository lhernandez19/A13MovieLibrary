﻿using System;
using A13MovieLibrary.Data;
using A13MovieLibrary.Menus;
using Microsoft.Extensions.DependencyInjection;

namespace A13MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to WCTC movie library!\n");

             var serviceProvider = new ServiceCollection()
                .AddSingleton<IRepository, Repository>()
                .AddSingleton<IContext, Contexts>()
                .AddSingleton<IMenu, Menu>()
                .BuildServiceProvider();

            var menu = serviceProvider.GetService<IMenu>();

            menu.DisplayMenu();
        
            Console.WriteLine("\nThank you for using the movie library!\n");
        }
    }
}
