using ExcelReader;
using Microsoft.EntityFrameworkCore;

var contextOptions = new DbContextOptionsBuilder<ExcelDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HenleyExcelReader;ConnectRetryCount=0")
    .Options;

using var db = new ExcelDbContext(contextOptions);
var excelService = new ExcelService(db);
excelService.RestartDatabase();

var filename = args.Length > 1 ? args[1] : "Jeopardy.xlsx";
excelService.ExcelToDatabase(filename);

SpectreUi.DisplayData(excelService.GetAll());