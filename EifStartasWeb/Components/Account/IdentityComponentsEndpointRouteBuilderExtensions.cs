using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using EifStartasWeb.Components.Account.Pages;
using EifStartasWeb.Components.Account.Pages.Manage;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Microsoft.AspNetCore.Routing;

internal static class IdentityComponentsEndpointRouteBuilderExtensions
{
    // These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var accountGroup = endpoints.MapGroup("/Account");

        accountGroup.MapPost("/PerformExternalLogin", (
            HttpContext context,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromForm] string provider,
            [FromForm] string returnUrl) =>
        {
            IEnumerable<KeyValuePair<string, StringValues>> query =
            [
                new("ReturnUrl", returnUrl),
                new("Action", ExternalLogin.LoginCallbackAction)
            ];

            var redirectUrl = UriHelper.BuildRelative(
                context.Request.PathBase,
                "/Account/ExternalLogin",
                QueryString.Create(query));

            provider = TemporaryFluentButtonFix(provider);

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return TypedResults.Challenge(properties, [provider]);
        });

        //  accountGroup.MapPost("/Logout", async (
        //    ClaimsPrincipal user,
        //   [FromServices] SignInManager<IdentityUser> signInManager,
        //    [FromForm] string returnUrl) =>
        //  {
        //
        //       await signInManager.SignOutAsync();
        //     return TypedResults.LocalRedirect($"~/{returnUrl}");
        // });

        // accountGroup.MapPost("/Logout", async (
        //     ClaimsPrincipal user,
        //     [FromServices] SignInManager<IdentityUser> signInManager,
        //     [FromServices] IAuthenticationService authenticationService,
        //     HttpContext httpContext,
        //     [FromForm] string returnUrl) =>
        // {
        //     // Sign out of local authentication
        //     await signInManager.SignOutAsync();
        //
        //     Console.WriteLine($"Return URL: {returnUrl}"); // Log the return URL
        //
        //     // Sign out of the external authentication (e.g., Azure AD)
        //     var authProperties = new AuthenticationProperties { RedirectUri = returnUrl }; // Set properties as needed
        //     await authenticationService.SignOutAsync(httpContext, OpenIdConnectDefaults.AuthenticationScheme, authProperties);
        //
        //     // Optionally, sign out from any other cookie schemes if used
        //     await authenticationService.SignOutAsync(httpContext, "Identity.Application", authProperties);
        //     await authenticationService.SignOutAsync(httpContext, "Identity.External", authProperties);
        //     await authenticationService.SignOutAsync(httpContext, "Cookies", authProperties); // Add this if you're using another cookie scheme
        //
        //     
        //     if (string.IsNullOrEmpty(returnUrl) || !returnUrl.StartsWith("/"))
        //     {
        //         returnUrl = "/auth";
        //     }
        //
        //     
        //     // Redirect to the return URL
        //     return TypedResults.LocalRedirect(returnUrl);
        // });
        //
        accountGroup.MapPost("/Logout", async (
            ClaimsPrincipal user,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IAuthenticationService authenticationService,
            HttpContext httpContext,
            [FromForm] string returnUrl) =>
        {
            // Sign out of local authentication
            await signInManager.SignOutAsync();
        
            Console.WriteLine($"Return URL: {returnUrl}"); // Log the return URL
        
            // Sign out of the external authentication (e.g., Azure AD)
            //
            var authProperties = new AuthenticationProperties { RedirectUri = "https://localhost:5111/signout-callback" };
            
            
           await authenticationService.SignOutAsync(httpContext, OpenIdConnectDefaults.AuthenticationScheme, authProperties);
        
            // Optionally, sign out from any other cookie schemes if used
           await authenticationService.SignOutAsync(httpContext, "Identity.Application", authProperties);
           await authenticationService.SignOutAsync(httpContext, "Identity.External", authProperties);
           await authenticationService.SignOutAsync(httpContext, "Cookies", authProperties); // Add this if you're using another cookie scheme
        
            // Redirect to Azure AD logout endpoint
            var logoutUri = $"https://login.microsoftonline.com/43864a51-470c-4314-a939-57bf31cb1138/oauth2/v2.0/logout?post_logout_redirect_uri=https://localhost:5111/signout-callback";
        
            // Redirect to the Azure AD logout endpoint
            return TypedResults.Redirect(logoutUri);
        });
        
        
        // accountGroup.MapPost("/Logout", async (
        //     ClaimsPrincipal user,
        //     [FromServices] SignInManager<IdentityUser> signInManager,
        //     [FromServices] IAuthenticationService authenticationService,
        //     HttpContext httpContext) =>
        // {
        //     // Determine the authentication scheme
        //     var authScheme = user.Identity?.AuthenticationType;
        //
        //         // var authProperties = new AuthenticationProperties(); // Create empty auth properties
        //
        //     if (authScheme == OpenIdConnectDefaults.AuthenticationScheme)
        //     {
        //          var authProperties = new AuthenticationProperties { RedirectUri = "http://localhost:5111/signout-callback" };
        //          
        //          
        //         await authenticationService.SignOutAsync(httpContext, OpenIdConnectDefaults.AuthenticationScheme, authProperties);
        //         
        //          // Optionally, sign out from any other cookie schemes if used
        //        // await authenticationService.SignOutAsync(httpContext, "Identity.Application", authProperties);
        //        // await authenticationService.SignOutAsync(httpContext, "Identity.External", authProperties);
        //         //await authenticationService.SignOutAsync(httpContext, "Cookies", authProperties); // Add this if you're using another cookie scheme
        //         
        //          // Redirect to Azure AD logout endpoint
        //          var logoutUri = $"https://login.microsoftonline.com/43864a51-470c-4314-a939-57bf31cb1138/oauth2/v2.0/logout?post_logout_redirect_uri=http://localhost:5111/signout-callback";
        //         
        //          // Redirect to the Azure AD logout endpoint
        //          return TypedResults.Redirect(logoutUri);
        //         //return TypedResults.Redirect(logoutUri);
        //     }
        //     else
        //     {
        //         // User signed in locally, log out from local only
        //         await signInManager.SignOutAsync();
        //         //await authenticationService.SignOutAsync(httpContext, "Identity.Application", authProperties);
        //
        //         return TypedResults.Redirect("/");
        //     }
        // });
        
        
        var manageGroup = accountGroup.MapGroup("/Manage").RequireAuthorization();

        manageGroup.MapPost("/LinkExternalLogin", async (
            HttpContext context,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromForm] string provider) =>
        {
            // Clear the existing external cookie to ensure a clean login process
            await context.SignOutAsync(IdentityConstants.ExternalScheme);

            var redirectUrl = UriHelper.BuildRelative(
                context.Request.PathBase,
                "/Account/Manage/ExternalLogins",
                QueryString.Create("Action", ExternalLogins.LinkLoginCallbackAction));

            provider = TemporaryFluentButtonFix(provider);

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl,
                signInManager.UserManager.GetUserId(context.User));
            return TypedResults.Challenge(properties, [provider]);
        });

        var loggerFactory = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
        var downloadLogger = loggerFactory.CreateLogger("DownloadPersonalData");

        manageGroup.MapPost("/DownloadPersonalData", async (
            HttpContext context,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] AuthenticationStateProvider authenticationStateProvider) =>
        {
            var user = await userManager.GetUserAsync(context.User);
            if (user is null)
            {
                return Results.NotFound($"Unable to load user with ID '{userManager.GetUserId(context.User)}'.");
            }

            var userId = await userManager.GetUserIdAsync(user);
            downloadLogger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(IdentityUser).GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var logins = await userManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }

            personalData.Add("Authenticator Key", (await userManager.GetAuthenticatorKeyAsync(user))!);
            var fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

            context.Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
            return TypedResults.File(fileBytes, contentType: "application/json", fileDownloadName: "PersonalData.json");
        });

        return accountGroup;
    }

    private static string TemporaryFluentButtonFix(string provider)
    {
        var providers = provider.Split(',');

        var duplicateProvider = providers.GroupBy(p => p)
            .Where(g => g.Count() == 2)
            .Select(g => g.Key);

        if (!duplicateProvider.Any())
        {
            return providers.FirstOrDefault() ?? string.Empty; // Fallback to first provider or empty string
        }

        return duplicateProvider.First();
    }
}