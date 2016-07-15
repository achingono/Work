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
    public class ProjectsController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<ProjectModel> Get()
        {
            // load projects from the database
            // and transform them to models
            return this.DbContext.Projects
                       .ToList()
                       .Select(x => x.To<Project, ProjectModel>());
        }

        // GET api/<controller>/<id>
        public ProjectModel Get(int id)
        {
            // find matching project 
            // and transform it to the model
            return this.DbContext.Projects
                       .Find(id)
                       .To<Project, ProjectModel>();
        }

        // POST api/<controller>
        public ProjectModel Post(ProjectModel model)
        {
            // find matching project
            var project = new Project();

            // update project fields with those supplied in the model
            project.UpdateFrom(model);

            // add project to database
            this.DbContext.Projects.Add(project);
            this.DbContext.SaveChanges();

            // return the project back as a model
            return project.To<Project, ProjectModel>();
        }

        // PUT api/<controller>/5
        public ProjectModel Put(int id, ProjectModel model)
        {
            // find matching project
            var project = this.DbContext.Projects.Find(id);

            // update project fields with those supplied in the model
            project.UpdateFrom(model);

            // update the database
            this.DbContext.SaveChanges();

            // return the project back as a model
            return project.To<Project, ProjectModel>();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            // find matching project
            var project = this.DbContext.Projects.Find(id);

            // delete the project
            this.DbContext.Projects.Remove(project);

            // update the database
            this.DbContext.SaveChanges();
        }
    }
}