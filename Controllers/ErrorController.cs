using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/404")]
    public IActionResult Error404()
    {
        return View("Error404"); // Razor View under Views/Shared/Error404.cshtml
    }

    [Route("Error/{code}")]
    public IActionResult Generic(int code)
    {
        if (code == 404)
            return RedirectToAction("Error404");

        return View("GenericError");
    }
}
