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
            // Make sure that the ApplicationDbContext database is created
            var hasSuccess = appContext.Database.EnsureCreated();
            if (!hasSuccess)
                throw new ApplicationException("The ApplicationDbContext database has not been created. Please check the logs and try again.");

            // Make sure that the CorwordsDbContext database is created
            hasSuccess = context.Database.EnsureCreated();
            if (!hasSuccess)
                throw new ApplicationException("The CorwordsDbContext database has not been created. Please check the logs and try again.");

            // Seed the defaults if they do not exist
        }
    }
}