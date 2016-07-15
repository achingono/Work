using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Work.Logic;
using Work.Data;

namespace Work.Web.Api.Controllers
{
    public class TeamsController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<TeamModel> Get()
        {
            // load teams from the database
            // and transform them to models
            return this.DbContext.Teams
                       .ToList()
                       .Select(x => x.To<Team, TeamModel>());
        }

        // GET api/<controller>/<id>
        public TeamModel Get(int id)
        {
            // find matching team 
            // and transform it to the model
            return this.DbContext.Teams
                       .Find(id)
                       .To<Team, TeamModel>();
        }

        // POST api/<controller>
        public TeamModel Post(TeamModel model)
        {
            // find matching team
            var team = new Team();

            // update team fields with those supplied in the model
            team.UpdateFrom(model);

            // add team to database
            this.DbContext.Teams.Add(team);
            this.DbContext.SaveChanges();

            // return the team back as a model
            return team.To<Team, TeamModel>();
        }

        // PUT api/<controller>/5
        public TeamModel Put(int id, TeamModel model)
        {
            // find matching team
            var team = this.DbContext.Teams.Find(id);

            // update team fields with those supplied in the model
            team.UpdateFrom(model);

            // update the database
            this.DbContext.SaveChanges();

            // return the team back as a model
            return team.To<Team, TeamModel>();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            // find matching team
            var team = this.DbContext.Teams.Find(id);

            // delete the team
            this.DbContext.Teams.Remove(team);

            // update the database
            this.DbContext.SaveChanges();
        }
    }
}