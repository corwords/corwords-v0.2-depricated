using System;

namespace Corwords.Web.Data
{
    public static class DbInitializer
    {
        public static void DestroyDatabases(ApplicationDbContext appContext, CorwordsDbContext context)
        {
            appContext.TrashDatabaseSource();
            context.TrashDatabaseSource();
        }

        public static void Initialize(ApplicationDbContext appContext, CorwordsDbContext context)
        {
            // Ensure databases are created
            appContext.Database.EnsureCreated();
            context.Database.EnsureCreated();

            // Seed the defaults if they do not exist
        }
    }
}