namespace UserManagement.Infrastructure.Persistence.Csv;

internal static class CsvHelpers
{
  internal static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

  /// <summary>
  /// Splits the specified string into an array of lines, removing any empty entries.
  /// </summary>
  /// <param name="content">The string to split into lines. Can contain line breaks using either '\n' or '\r\n'.</param>
  /// <returns>An array of strings, each representing a non-empty line from the input. The array is empty if the input contains no
  /// non-empty lines.</returns>
  internal static string[] Lines(string content)
      => content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

  /// <summary>
  /// Determines whether the specified line represents a header row based on known header prefixes.
  /// </summary>
  /// <remarks>A line is considered a header if it begins with "UserId" or "AuditId", using a case-insensitive
  /// comparison.</remarks>
  /// <param name="line">The line of text to evaluate. Cannot be null.</param>
  /// <returns>true if the line starts with a recognized header prefix; otherwise, false.</returns>
  internal static bool IsHeader(string line)
      => line.StartsWith("UserId", StringComparison.OrdinalIgnoreCase)
         || line.StartsWith("AuditId", StringComparison.OrdinalIgnoreCase);

  internal static string NormalizeCell(string s) => s.Trim();
}
