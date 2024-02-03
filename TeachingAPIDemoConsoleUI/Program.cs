using Newtonsoft.Json;
using TeachingAPIDemoConsoleUI.Factory;

namespace TeachingAPIDemoConsoleUI;

internal class Program
{
    private const string URL = "http://localhost:5000";

    static void Main(string[] args)
    {
        ConsoleLogging.PassMessage("Do you want to view dogs or books?");
        var userEndpoint = Console.ReadLine() ?? string.Empty;

        var request = RequestMessageBuilder.CreateRequest()
                                            .AsMethod(HttpMethod.Get)
                                            .ForUrl(URL)
                                            .AtEndpoint(userEndpoint)
                                            .Build();

        var response = SendRequest(request).Result;

        var body = GetJsonString(response).Result;

        var objects = Desearialize(body, userEndpoint); //List<string> <generics>

        foreach (var item in objects)
        {
            Console.WriteLine(item.ToJson());
        }
    }

    private async static Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
    {
        var response = await HttpClientFactory.Create().SendAsync(request);
        return response;
    }

    private async static Task<string> GetJsonString(HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        return json;
    }

    private static IList<IApiType> Desearialize(string json, string type)
    {
        if (type.Contains("book"))
        {
            return (IList<IApiType>)(JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>());
        }
        else
        {
            return (IList<IApiType>)(JsonConvert.DeserializeObject<List<Dog>>(json) ?? new List<Dog>());
        }
    }
}
