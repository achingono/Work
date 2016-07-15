using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work.Logic;

namespace Work.Web.Controllers
{
    public class ClientsController : BaseController
    {
        /// <summary>
        /// GET: /Clients
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /clients/detail/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            return View();
        }

        /// <summary>
        /// GET: /clients/add
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new ClientModel();
            model.Initialize();
            ViewBag.Json = model.ToJson();

            return View();
        }

        /// <summary>
        /// GET: /clients/edit/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}