using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work.Web.Filters
{
    public class DefaultsActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var viewData = filterContext.Controller.ViewData;
            var routeData = filterContext.RouteData;

            viewData.Add("ApplicationName", ConfigurationManager.AppSettings["Application.Name"]);
            viewData.Add("Class", routeData.Values["controller"]);
            viewData.Add("Title", routeData.Values["controller"].ToString().ToTitleCase());
            viewData.Add("Script", string.Format("/{0}/{1}/script", routeData.Values["controller"], routeData.Values["action"]));

            if (routeData.Values.ContainsKey("id"))
            {
                viewData.Add("Api", string.Format("/api/{0}/{1}", routeData.Values["controller"], routeData.Values["id"]));
            }
            else
            {
                viewData.Add("Api", string.Format("/api/{0}", routeData.Values["controller"]));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}