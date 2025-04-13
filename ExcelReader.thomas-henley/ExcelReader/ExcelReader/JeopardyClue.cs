namespace ExcelReader;

public class JeopardyClue
{
    // Show Number	 Air Date	 Round	 Category	 Value	 Question	 Answer
    public int Id { get; set; }
    public required int ShowNumber { get; set; }
    public required DateTime Date { get; set; }
    public required string Round { get; set; }
    public required string Category { get; set; }
    public required string Value { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
}