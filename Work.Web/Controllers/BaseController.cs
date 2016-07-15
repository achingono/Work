using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work.Data;
using Work.Web.Filters;

namespace Work.Web.Controllers
{
    [DefaultsActionFilter]
    public abstract class BaseController : Controller
    {
        private Lazy<Context> _context = new Lazy<Context>();
        public Context DbContext
        {
            get
            {
                return _context.Value;
            }
        }

        public FileResult Script()
        {
            var path = string.Format("~/Views/{0}/{1}.js",
                                    this.RouteData.Values["controller"],
                                    this.RouteData.Values["file"]);
            return File(Server.MapPath(path), "text/javascript");
        }
    }
}