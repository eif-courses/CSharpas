using System.Globalization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using EifStartasWeb.Components;
using EifStartasWeb.Components.Account;
using EifStartasWeb.Data;
using EifStartasWeb.Services;
using EifStartasWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

//builder.Services.AddScoped<IStorageService, CloudflareR2Service>();

builder.Services.AddScoped<IFileUploadService, FileUploadService>();

builder.Services.AddScoped<IDataImportService, ImportFromExcelService>();


// Load Azure AD configuration
//var azureAdConfig = builder.Configuration.GetSection("AzureAd");

builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme; // Ensures local identity login works
        options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect(options =>
    {
        options.ClientId = builder.Configuration["AzureAd:ClientId"];
        options.ClientSecret = builder.Configuration["AzureAd:ClientSecret"];
        options.Authority = $"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}/v2.0";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.Scope.Add("offline_access");
        options.Scope.Add("User.Read");
        options.CallbackPath = "/signin-oidc";

        options.Events = new OpenIdConnectEvents
        {
            OnRedirectToIdentityProvider = context => { return Task.CompletedTask; },
            OnTokenValidated = context => { return Task.CompletedTask; },
            OnRemoteFailure = context =>
            {
                context.Response.Redirect("/");
                context.HandleResponse();
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();


// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


// https://github.com/Azure-Samples/ms-identity-docs-code-dotnet/tree/main/web-app-blazor-server


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddSignInManager()
//     .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<IdentityUser>, IdentityNoOpEmailSender>();


// Kalbų lokalizavimas

builder.Services.AddLocalization(options => { options.ResourcesPath = "Localization"; });

var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("lt-LT")
};


var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("lt-LT"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//var passwordHasher = new PasswordHasher<IdentityUser>();

// Hash the password
//string password = "Kolegija1@";
//string hashedPassword = passwordHasher.HashPassword(null, password);

// Output the hashed password
//Console.WriteLine(hashedPassword);


app.MapControllers();

app.UseHttpsRedirection();
app.UseRequestLocalization(localizationOptions);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();