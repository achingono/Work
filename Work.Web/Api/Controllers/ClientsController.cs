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
    public class ClientsController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<ClientModel> Get()
        {
            // load clients from the database
            // and transform them to models
            return this.DbContext.Clients
                       .ToList()
                       .Select(x => x.To<Client, ClientModel>());
        }

        // GET api/<controller>/<id>
        public ClientModel Get(int id)
        {
            // find matching client 
            // and transform it to the model
            return this.DbContext.Clients
                       .Find(id)
                       .To<Client, ClientModel>();
        }

        // POST api/<controller>
        public ClientModel Post(ClientModel model)
        {
            // find matching client
            var client = new Client();

            // update client fields with those supplied in the model
            client.UpdateFrom(model);

            // add client to database
            this.DbContext.Clients.Add(client);
            this.DbContext.SaveChanges();

            // return the client back as a model
            return client.To<Client, ClientModel>();
        }

        // PUT api/<controller>/5
        public ClientModel Put(int id, ClientModel model)
        {
            // find matching client
            var client = this.DbContext.Clients.Find(id);

            // update client fields with those supplied in the model
            client.UpdateFrom(model);

            // update the database
            this.DbContext.SaveChanges();

            // return the client back as a model
            return client.To<Client, ClientModel>();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            // find matching client
            var client = this.DbContext.Clients.Find(id);

            // delete the client
            this.DbContext.Clients.Remove(client);

            // update the database
            this.DbContext.SaveChanges();
        }
    }
}