cd "My Files\Projects\Corwords\Corwords\Corwords.Web"

dotnet ef migrations add InitialCorwordsCreate --context CorwordsDbContext

dotnet ef database drop --force --context ApplicationDbContext
dotnet ef database update --context CorwordsDbContext