using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NLipsum.Core;

namespace Work.Data.Initializers
{
    /// <summary>
    /// Summary description for Initializer
    /// </summary>
    public class DropCreate : DropCreateDatabaseAlways<Context> //DropCreateDatabaseIfModelChanges<Context>
    {

        /// <summary>
        /// Seed the database
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(Context context)
        {
            base.Seed(context);

            // populate data in a separate thread
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                context.SeedClients();
                context.SeedTeams();
                context.SeedEmployees();
                context.SeedProjects();
                context.SeedTasks();
                System.Diagnostics.Debugger.Break();
            });
        }
    }
}