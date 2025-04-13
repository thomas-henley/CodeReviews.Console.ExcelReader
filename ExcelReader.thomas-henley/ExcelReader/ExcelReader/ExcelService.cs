using OfficeOpenXml;
using static OfficeOpenXml.ExcelPackage;

namespace ExcelReader;

public class ExcelService
{
    private readonly ExcelDbContext _context;

    public ExcelService(ExcelDbContext context)
    {
        _context = context;
        License.SetNonCommercialPersonal("Thomas Henley");
    }

    /// <summary>
    /// Reads the data from an Excel file into the database.
    /// </summary>
    /// <param name="filename">The Excel file to be read.</param>
    public void ExcelToDatabase(string filename)
    {
        Console.WriteLine("Reading " + filename + " into database...");

        using var package = new ExcelPackage(filename);
        var worksheet = package.Workbook.Worksheets[0];
        var rowCount = worksheet.Dimension.End.Row;

        for (var row = 2; row <= rowCount; row++)
        {
            var clue = new JeopardyClue()
            {
                ShowNumber = int.Parse(worksheet.Cells[row, 1].Value.ToString()!),
                Date = DateTime.Parse(worksheet.Cells[row, 2].Value.ToString()!),
                Round = worksheet.Cells[row, 3].Value.ToString()!,
                Category = worksheet.Cells[row, 4].Value.ToString()!,
                Value = worksheet.Cells[row, 5].Value.ToString()!,
                Question = worksheet.Cells[row, 6].Value.ToString()!,
                Answer = worksheet.Cells[row, 7].Value.ToString()!,
            };
    
            _context.JeopardyClues.Add(clue);
            Console.Write(".");
        }
        var changes = _context.SaveChanges();
        Console.WriteLine($"{changes} records updated.");
    }

    /// <summary>
    /// Deletes the existing database and recreates it.
    /// </summary>
    public void RestartDatabase()
    {
        if (_context.Database.EnsureDeleted())
        {
            Console.WriteLine("Database has been deleted.");
        }

        if (_context.Database.EnsureCreated())
        {
            Console.WriteLine("Database has been created.");
        }
    }

    /// <summary>
    /// Gets the collection of Jeopardy clues from the database.
    /// </summary>
    /// <returns>A List of JeopardyClues containing the entire table.</returns>
    public List<JeopardyClue> GetAll()
    {
        return _context.JeopardyClues.ToList();
    }
}