using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Work.Web.Api.Controllers
{
    public class HomeController : BaseController
    {
        // GET api/<controller>
        public dynamic Get()
        {
            // load clients from the database
            // and transform them to models
            return new
            {
                Clients = this.DbContext.Clients.Count(),
                Projects = this.DbContext.Projects.Count(),
                Employees = this.DbContext.Employees.Count(),
                Teams = this.DbContext.Teams.Count()
            };
        }
    }
}
