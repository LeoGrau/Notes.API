namespace Notes.API.Shared.Extensions;

public static class StringExtension
{
    public static string? ToSnakeCase(this string? text)
    {
        static IEnumerable<char> Convert(CharEnumerator? letter)
        {
            if (!letter.MoveNext())
                yield break;
            yield return char.ToLower(letter.Current);
            while (letter.MoveNext())
            {
                if (char.IsUpper(letter.Current))
                    yield return '_';
                yield return char.ToLower(letter.Current);
            }
        }
        return new string(Convert(text.GetEnumerator()).ToArray());
    }
}