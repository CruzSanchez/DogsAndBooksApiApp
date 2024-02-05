using DogsAndBooksApi.CoreLibrary.Helpers;
using DogsAndBooksApi.CoreLibrary.Models;

namespace DogsAndBooksApi.CoreLibrary.Factories;
public static class ApiTypeFactory
{
    public static IApiType CreateDogOrBook(string type, Action<string, StatusCode> requestDataFromUser, Func<string> getUserInput,
                                         Func<string, List<int>> parser)
    {
        return type == "Book" ? CreateBook(requestDataFromUser, getUserInput) : CreateDog(requestDataFromUser, getUserInput, parser);
    }

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

