using Newtonsoft.Json;

namespace TeachingAPIDemoConsoleUI;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Do you want to view dogs or books?");
        var userEndpoint = Console.ReadLine();

        HttpClient client = new();
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://43a3-2600-6c58-487f-aab8-4869-313b-83ac-a9a4.ngrok-free.app/{userEndpoint}")
        };

        var response = client.SendAsync(request).Result;

        var body = response.Content.ReadAsStringAsync().Result;

        var books = JsonConvert.DeserializeObject<List<Book>>(body); //List<string> <generics>

        foreach (var book in books)
        {
            Console.WriteLine(book.ToJson());
        }
    }
}
