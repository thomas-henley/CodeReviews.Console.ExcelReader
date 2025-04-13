using Microsoft.EntityFrameworkCore;

namespace ExcelReader;

public class ExcelDbContext(DbContextOptions<ExcelDbContext> options) : DbContext(options)
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HenleyExcelReader;ConnectRetryCount=0");
    }
    
    public DbSet<JeopardyClue> JeopardyClues { get; set; }
}