using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace EifStartasWeb.Controllers;

[Route("[controller]/[action]")]
public class CultureController : Controller
{
    [HttpPost]
    public IActionResult SetCulture(string culture, string redirectUri)
    {
        // Validate the culture parameter
        if (string.IsNullOrEmpty(culture))
        {
            return BadRequest("Culture is required.");
        }

        // Validate the redirectUri parameter
        if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
        {
            return BadRequest("Invalid redirect URI.");
        }

        try
        {
            // Set the culture cookie
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    HttpOnly = true, // Prevent JavaScript access to the cookie
                    Secure = true,   // Use Secure flag for HTTPS
                    SameSite = SameSiteMode.Lax // Adjust SameSite policy as per requirement
                }
            );
        }
        catch (CultureNotFoundException)
        {
            return BadRequest("Invalid culture specified.");
        }

        // Redirect to the specified URI
        return LocalRedirect(redirectUri);
    }
}