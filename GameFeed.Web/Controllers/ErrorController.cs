using System.Net;
using System.Web.Mvc;
using GameFeed.Web.Models;

namespace GameFeed.Web.Controllers {

    public class ErrorController : Controller {

        public ActionResult NotFound() {
            ErrorModel model = new ErrorModel {
                HttpStatusCode = HttpStatusCode.NotFound
            };

            Response.StatusCode = 404;

            return View("Error", model);
        }

        public ActionResult InternalServerError() {
            ErrorModel model = new ErrorModel {
                HttpStatusCode = HttpStatusCode.InternalServerError
            };

            Response.StatusCode = 500;

            return View("Error", model);
        }
    }
}