namespace TeachingAPIDemoConsoleUI.Extensions.StringExtensions
{
    internal static class StringExtension
    {
        public static bool IsNullOrWhitespaceOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str) && string.IsNullOrEmpty(str);
        }
    }
}
