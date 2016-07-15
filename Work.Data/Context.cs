using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;

namespace Work.Data
{
    /// <summary>
    /// Summary description for ClassName
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Default constructor sets the default connection string
        /// </summary>
        public Context()
            : base("Name=Work") { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }

}