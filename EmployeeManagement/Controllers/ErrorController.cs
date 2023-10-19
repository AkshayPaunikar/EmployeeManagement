using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult HTTPStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Not Found";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    break;
            }

            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var execption = HttpContext.Features.Get<IExceptionHandlerFeature>();

            ViewBag.Path = execption.Path;
            ViewBag.Trace = execption.Error.StackTrace;
            return View("Error");
        }
    }
}
