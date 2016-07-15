using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Work.Data.Initializers
{
    /// <summary>
    /// Configuration object to determine how migrations will be executed
    /// </summary>
    public class Configuration : DbMigrationsConfiguration<Context>
    {
        /// <summary>
        /// Default constructor to set default settings
        /// </summary>
        public Configuration()
            : base()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Initialize data in the database when first created.
        /// </summary>
        /// <param name="context">The data context to be initialized</param>
        protected override void Seed(Context context)
        {
            base.Seed(context);
            context.SeedClients();
            context.SeedTeams();
            context.SeedEmployees();
            context.SeedProjects();
            context.SeedTasks();
        }
    }
}