namespace NoviCode;

// Single place for the connection string used by the DbContext and the Dapper MERGE.
// Points at the local SQL Server running in Docker (container `ms-sql`, port 1433).
// NOTE: this is a local dev credential — move it to user-secrets / appsettings / an
// environment variable before using anything like this outside the lab.
public static class DbConnection
{
	public const string ConnectionString =
		"Server=localhost,1433;Database=NoviCode;User Id=sa;Password=Test123@;TrustServerCertificate=True";
}
