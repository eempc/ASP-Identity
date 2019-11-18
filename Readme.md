# Actions
* Instructions on how to add a custom user identity with additional fields such as name and date of birth

https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-3.0&tabs=visual-studio

## Steps

1. Create one of the following:
    * A standard Razor web app (non-MVC, non-SPA) without authorisation
    * A React web app with authorisation
2. Right-click project and add Identity Scaffold, the options to tick are: Account/Register and Account/Manage/Index
3. Create both New Data context class and New user class (not for React), no need to modify the layout file then Add
4. Migrate and update the newly generated database context
5. Add app.UseAthentication(); to Startup.cs
6. Add to the header of the layout file: `<partial name="_LoginPartial" />`
7. Find user model, e.g. WebApplication1User.cs in the Areas/ folder
8. Add [PersonalData] properties to your user class such as Name and DOB
9. Update the razor pages and associated cs files:
    * Manage/Index.cshtml changes required:

    ```Add properties to InputModels within the constructor, LoadAsync (not GetAsync), PostAsync - will have to refer to the document for this one, will mark the new stuff with asterisks```
    * Register.cshtml changes required:

    ```Add properties to InputModels within the constructor, and onPostAsync```
10. Build then migrate

-----------

## Unifying the DBcontext

* DBContext services startup will in IdentityHostingStartup.cs
* The contents of this can be moved back to Startup.cs with a few differences.

```[assembly: HostingStartup(typeof(WebApplication1.Areas.Identity.IdentityHostingStartup))] // This bit may not be required, it can be commented out
namespace WebApplication1 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<WebApplication1Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("WebApplication1ContextConnection")));

            services.AddDefaultIdentity<WebApplication1User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<WebApplication1Context>();

            services.AddRazorPages();
        }
        ... etc ...
 ```


-----------

# Adding the main model to work with

* If you did the previous steps to move IHostingStartup contents, then the model context will be lumped into the identity db context. This is the same as the original method when the ApplicationDBContext was created by the VS wizard
* If not, then you would need to make a new second database for your main model, as it will not allow access to the identity model.

1. Add the main Model that you will be working with
2. Scaffold that model
3. Add services.AddScoped<IModelName, ModelName>(); (maybe not necessary)
4. Add-Migration then Update-Database
* Create.cshtml has this hidden field UserID which will be recorded in the main country model as who added that country
    * I think I already tried to set this field within the OnPostAsync() but it did not work
```
<form method="post">
    <!-- Create a hidden <input> which will assign the user ID -->
    <input type="hidden" asp-for="Country.UserID" value="@Model.userId" />
```
* And the .cs file
```
public string userId; // Declare the user ID in order to assign it later
public IActionResult OnGet()
{
    userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Initialise user ID, then can use it in the CSHMTL file
    return Page();
}
```


