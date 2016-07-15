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
    public class EmployeesController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<EmployeeModel> Get()
        {
            // load employees from the database
            // and transform them to models
            return this.DbContext.Employees
                       .ToList()
                       .Select(x => x.To<Employee, EmployeeModel>());
        }

        // GET api/<controller>/<id>
        public EmployeeModel Get(int id)
        {
            // find matching employee 
            // and transform it to the model
            return this.DbContext.Employees
                       .Find(id)
                       .To<Employee, EmployeeModel>();
        }

        // POST api/<controller>
        public EmployeeModel Post(EmployeeModel model)
        {
            // find matching employee
            var employee = new Employee();

            // update employee fields with those supplied in the model
            employee.UpdateFrom(model);

            // add employee to database
            this.DbContext.Employees.Add(employee);
            this.DbContext.SaveChanges();

            // return the employee back as a model
            return employee.To<Employee, EmployeeModel>();
        }

        // PUT api/<controller>/5
        public EmployeeModel Put(int id, EmployeeModel model)
        {
            // find matching employee
            var employee = this.DbContext.Employees.Find(id);

            // update employee fields with those supplied in the model
            employee.UpdateFrom(model);

            // update the database
            this.DbContext.SaveChanges();

            // return the employee back as a model
            return employee.To<Employee, EmployeeModel>();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            // find matching employee
            var employee = this.DbContext.Employees.Find(id);

            // delete the employee
            this.DbContext.Employees.Remove(employee);

            // update the database
            this.DbContext.SaveChanges();
        }
    }
}