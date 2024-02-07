using DogsAndBooksApi.CoreLibrary.Helpers;
using DogsAndBooksApi.CoreLibrary.Models;

namespace DogsAndBooksApi.CoreLibrary.Factories;
public static class ApiTypeFactory
{
    /// <summary>
    /// Creates the specified type
    /// </summary>
    /// <param name="type">The type to create</param>
    /// <param name="requestDataFromUser">A function that asks the user for data</param>
    /// <param name="getUserInput">A function that allows a user to input the requested data</param>
    /// <param name="parser">A function that parses a string and returns a list of int</param>
    /// <returns></returns>
    public static IApiType CreateDogOrBook(string type, Action<string, StatusCode> requestDataFromUser, Func<string> getUserInput,
                                         Func<string, List<int>> parser)
    {
        return type == "Book" ? CreateBook(requestDataFromUser, getUserInput) : CreateDog(requestDataFromUser, getUserInput, parser);
    }

    /// <summary>
    /// Creates a Dog
    /// </summary>
    /// <param name="requestDataFromUser">A function that asks the user for data such as the name or description of the book</param>
    /// <param name="getUserInput">A function that allows a user to input the requested data</param>
    /// <param name="parser">A function that parses a string and returns a list of int which represents the dogs friend list</param>
    /// <returns>A Dog</returns>
    private static IApiType CreateDog(Action<string, StatusCode> requestDataFromUser, Func<string> getUserInput, Func<string, List<int>> parser)
    {
        Dog dog = new Dog();

        requestDataFromUser("Name", StatusCode.NoCode);
        dog.Name = getUserInput();

        requestDataFromUser("Owner Name?", StatusCode.NoCode);
        dog.Owner = getUserInput();

        requestDataFromUser("Breed?", StatusCode.NoCode);
        dog.Breed = getUserInput();

        requestDataFromUser("If this dog has friends please specify the ids of each dog separated by commas. ex: 1,2,44,31", StatusCode.NoCode);
        dog.Friends = parser(getUserInput());

        return dog;
    }

    /// <summary>
    /// Creates a book
    /// </summary>
    /// <param name="requestDataFromUser">A function that asks the user for data such as the name or description of the book</param>
    /// <param name="getUserInput">A function that allows a user to input the requested data</param>
    /// <returns>A book</returns>
    private static IApiType CreateBook(Action<string, StatusCode> requestDataFromUser, Func<string> getUserInput)
    {
        var book = new Book();

        requestDataFromUser("Title", StatusCode.NoCode);
        book.Title = getUserInput();

        requestDataFromUser("Description?", StatusCode.NoCode);
        book.Description = getUserInput();

        requestDataFromUser("Author Name?", StatusCode.NoCode);
        book.Author = getUserInput();

        requestDataFromUser("When was this created?\n mm/dd/yyyy", StatusCode.NoCode);

        DateOnly created;
        while (!DateOnly.TryParse(getUserInput(), out created))
        {
            requestDataFromUser("Invalid response, try again! mm/dd/yyyy", StatusCode.Error);
        }

        book.CreatedDate = created;

        requestDataFromUser("Genre?", StatusCode.NoCode);
        book.Genre = getUserInput();

        return book;
    }
}

