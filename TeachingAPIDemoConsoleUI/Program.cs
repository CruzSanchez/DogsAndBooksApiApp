using TeachingAPIDemoConsoleUI.ConsoleInteractions;
using TeachingAPIDemoConsoleUI.Infrastructure;

namespace TeachingAPIDemoConsoleUI;

internal class Program
{
    private static IList<Book> books = default;
    private static IList<Dog> dogs = default;

    static void Main(string[] args)
    {
        var menuKey = UserInteraction.MainMenu();
        var menuTwoKey = ConsoleKey.None;

        switch (menuKey)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                ApiPropertyHandler.UserEndpoint = "dog";
                menuTwoKey = UserInteraction.MenuLevelTwo();
                break;

            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                ApiPropertyHandler.UserEndpoint = "book";
                menuTwoKey = UserInteraction.MenuLevelTwo();
                break;

            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
                Environment.Exit(0);
                return;
        }

        if (ApiPropertyHandler.UserEndpoint == "dog")
        {
            UserInteraction.DogMenu(menuTwoKey, dogs);
        }
        else
        {
            UserInteraction.BookMenu(menuTwoKey);
        }

    }
}
