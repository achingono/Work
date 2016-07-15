using System.Data.Entity;

namespace Work.Data.Initializers
{
    public class MigrateToLatest : MigrateDatabaseToLatestVersion<Context, Configuration>
    {
        public void IntializeDatabase(Context context)
        {
            context.Database.Delete();
            base.InitializeDatabase(context);
        }
    }
}