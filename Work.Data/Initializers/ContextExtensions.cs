using NLipsum.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work.Data.Initializers
{
    public static class ContextExtensions
    {
        private static Random random = new Random();
        private static LipsumGenerator generator = new LipsumGenerator(Lipsums.LoremIpsum, false);

        /// <summary>
        /// Add clients to the database
        /// </summary>
        /// <param name="context"></param>
        public static void SeedClients(this Context context)
        {
            if (context.Clients.Any()) return;

            for (int i = 0; i < 50; i++)
            {
                var companyName = string.Join(" ", generator.GenerateWords(random.Next(1, 4))).ToTitleCase();
                var companyType = Constants.COMPANY_TYPES[random.Next(Constants.COMPANY_TYPES.Length)];
                var country = Constants.COUNTRIES[random.Next(Constants.COUNTRIES.Length)];

                if (!string.IsNullOrWhiteSpace(companyType))
                    companyName = string.Format("{0} {1}", companyName, companyType);

                context.Clients.Add(new Client
                {
                    Company = companyName,
                    Rate = random.Next(3, 9) * 25,
                    Notes = generator.GenerateLipsumHtml(random.Next(1, 5)),
                    Billing = CreateContact(companyName, country),
                    Mailing = CreateContact(companyName, country),
                    Technical = CreateContact(companyName, country),
                });
            }
            context.SaveChanges();
        }

        private static Contact CreateContact(string companyName, string country)
        {
            var male = (random.Next(0, 2) == 0);
            var firstName = string.Empty;
            var lastName = Constants.LAST_NAMES[random.Next(0, Constants.LAST_NAMES.Length)];

            if (male)
                firstName = Constants.MALE_NAMES[random.Next(0, Constants.MALE_NAMES.Length)];
            else
                firstName = Constants.FEMALE_NAMES[random.Next(0, Constants.FEMALE_NAMES.Length)];

            return new Contact
            {
                FirstName = firstName,
                Lastname = lastName,
                Email = string.Format("{0}{1}@{2}.com", firstName.First(), lastName, companyName.Split(' ').First()).ToLowerInvariant(),
                Address = new Address
                {
                    Street = string.Format("{0} {1} {2}", random.Next(1000), generator.GenerateWords(1)[0], Constants.STREET_TYPES[random.Next(Constants.STREET_TYPES.Length)]).ToTitleCase(),
                    City = string.Join(" ", generator.GenerateWords(random.Next(1, 3))).ToTitleCase(),
                    State = string.Join(" ", generator.GenerateWords(random.Next(1, 3))).ToTitleCase(),
                    Country = country,
                    PostalCode = string.Format("{0:00000}", random.Next(100000)),
                },
                Fax = string.Format("1-{0:000}-{1:000}-{2:0000}", random.Next(1000), random.Next(1000), random.Next(10000)),
                Phone = string.Format("1-{0:000}-{1:000}-{2:0000}", random.Next(1000), random.Next(1000), random.Next(10000)),
            };
        }

        /// <summary>
        /// Add teams to the database
        /// </summary>
        /// <param name="context"></param>
        public static void SeedTeams(this Context context)
        {
            if (context.Teams.Any()) return;

            foreach (var team in Constants.TEAM_NAMES)
            {
                context.Teams.Add(new Team
                {
                    Name = team,
                    Description = generator.GenerateSentences(1)[0],
                });
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Add employees to the database
        /// </summary>
        /// <param name="context"></param>
        public static void SeedEmployees(this Context context)
        {
            if (context.Employees.Any()) return;

            for (int i = 0; i < 20; i++)
            {
                var male = (random.Next(0, 2) == 0);
                var firstName = string.Empty;
                var lastName = Constants.LAST_NAMES[random.Next(0, Constants.LAST_NAMES.Length)];

                if (male)
                    firstName = Constants.MALE_NAMES[random.Next(0, Constants.MALE_NAMES.Length)];
                else
                    firstName = Constants.FEMALE_NAMES[random.Next(0, Constants.FEMALE_NAMES.Length)];

                var email = string.Format("{0}{1}@prosoftech.com", firstName.First(), lastName).ToLower();
                var teamName = Constants.TEAM_NAMES[random.Next(Constants.TEAM_NAMES.Length)];

                var employee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    HiredOn = DateTime.Today.AddDays(random.Next(1000) * -1),
                    Position = Constants.POSITIONS[Array.IndexOf(Constants.TEAM_NAMES, teamName)],
                    Teams = new List<Team>(),
                };

                var team = context.Teams.SingleOrDefault(t => t.Name == teamName);
                employee.Teams.Add(team);
                context.Employees.Add(employee);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Add projects to the database
        /// </summary>
        /// <param name="context"></param>
        public static void SeedProjects(this Context context)
        {
            if (context.Projects.Any()) return;

            foreach (var client in context.Clients)
            {
                var max = random.Next(1, 10);
                for (int i = 0; i < max; i++)
                {
                    context.Projects.Add(new Project()
                    {
                        Name = string.Join(" ", generator.GenerateWords(3)).ToTitleCase(),
                        Description = generator.GenerateSentences(1)[0],
                        Client = client,
                        Team = context.Teams.ToList().ElementAt(random.Next(Constants.TEAM_NAMES.Length)),
                    });
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Add tasks to the database
        /// </summary>
        /// <param name="context"></param>
        public static void SeedTasks(this Context context)
        {
            if (context.Tasks.Any()) return;

            var present = new string[] { "Create", "Discuss", "Test", "Troubleshoot", "Debug", "Resolve" };
            foreach (var project in context.Projects)
            {
                var max = random.Next(50);
                for (int i = 0; i < max; i++)
                {
                    var hours = 0.25 * random.Next(1, 100);
                    var startDate = DateTime.Today.AddDays(random.Next(1096) * -1);

                    context.Tasks.Add(new Task
                    {
                        Title = string.Format("{0} {1}", present[random.Next(present.Length)], string.Join(" ", generator.GenerateWords(random.Next(1, 10)))),
                        Description = string.Join(". ", generator.GenerateSentences(random.Next(1, 5))),
                        Project = project,
                        Estimated = new Estimation
                        {
                            StartDate = startDate,
                            DueDate = startDate.AddHours(hours).Date,
                            Hours = hours
                        },
                        State = (State)random.Next(7),
                        CreatedOn = DateTime.Now,
                        CreatedBy = "system",
                    });
                }
            }
            context.SaveChanges();
        }
    }
}
