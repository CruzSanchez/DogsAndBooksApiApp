using Newtonsoft.Json;

namespace TeachingAPIDemoConsoleUI
{
    public class Book : IApiType
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateOnly CreatedDate { get; set; }
        public string Genre { get; set; } = string.Empty;

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
