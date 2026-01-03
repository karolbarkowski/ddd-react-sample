namespace ProductsDomain.Application.Mappings
{
    internal static class StringExtensions
    {
        internal static string Capitalize(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();

            return char.ToUpper(str[0]) + str[1..];
        }
    }
}
