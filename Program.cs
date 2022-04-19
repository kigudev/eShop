// See https://aka.ms/new-console-template for more information

using Business.Services.Implementations;
using Data.Entities;
using eShop;

internal class Program
{
    private static void Main(string[] args)
    {
        var console = new eShopConsole();
        var showMenu = true;
        while (showMenu)
        {
            showMenu = console.MainMenu();
        }
    }
}