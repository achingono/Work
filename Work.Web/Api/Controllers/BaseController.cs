using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Work.Data;

namespace Work.Web.Api.Controllers
{
    public class BaseController : ApiController
    {
        private Lazy<Context> _context = new Lazy<Context>();
        public Context DbContext
        {
            get
            {
                return _context.Value;
            }
        }
    }
}
