using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NoviCode;

// Lets `dotnet ef migrations add` / `database update` build the context at design time
// without running the application.
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
	public AppDbContext CreateDbContext(string[] args)
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseSqlServer(DbConnection.ConnectionString)
			.Options;

		return new AppDbContext(options);
	}
}
