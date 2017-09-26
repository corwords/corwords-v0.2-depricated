cd "My Files\Projects\Corwords\Corwords\Corwords.Web"

dotnet ef migrations add InitialCreate --context CorwordsDbContext
dotnet ef database update --context CorwordsDbContext

dotnet ef database delete --context ApplicationDbContext