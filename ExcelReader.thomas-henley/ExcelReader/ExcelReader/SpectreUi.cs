using Spectre.Console;

namespace ExcelReader;

public static class SpectreUi
{
    public static void DisplayData(List<JeopardyClue> jeopardyClues)
    {
        var grid = new Grid();

        // Add header row
        var columns = new[] { "Show #", "Air Date", "Round", "Category", "Value", "Question", "Answer" };
        foreach (var _ in columns)
        {
            grid.AddColumn();
        }

        // Add data rows
        grid.AddRow(columns);
        foreach (var clue in jeopardyClues)
        {
            var cells = new[]
            {
                clue.ShowNumber.ToString(),
                clue.Date.ToString("dd/MM/yyyy"),
                clue.Round,
                clue.Category,
                clue.Value,
                EscapeMarkup(clue.Question),
                EscapeMarkup(clue.Answer)
            };
            Console.WriteLine(string.Join(" ", cells));
            grid.AddRow(cells);
        }

        // Write to Console
        AnsiConsole.Write(grid);
    }
    
    // Brackets [ and ] are special characters in Spectre so we need to escape them.
    private static string EscapeMarkup(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return input
            .Replace("[", "[[")
            .Replace("]", "]]");
    }
}