using CLHBankApp.Exceptions;
using CLHBankApp.Managers;
using CLHBankApp.Menus;
using System;
using System.Collections.Generic;

namespace CLHBankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            menu.Menu();

        }
    }
}
