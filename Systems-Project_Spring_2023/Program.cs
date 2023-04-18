using System.Globalization;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Systems_Project_Spring_2023.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Creating roles in DB
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();  // accessing RoleManager services
    var roles = new[] { "Admin", "Assistant", "Student" }; // creating array that will hold the roles

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role)) // Making sure the roles are not already created
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

// Creating default superuser account in DB
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();  // accessing UserManager services

    string superEmail = "admin@admin.com";
    string superPassword = "New2day2023!@#";

    if (await userManager.FindByEmailAsync(superEmail) == null)
    {
        var superUser = new IdentityUser();
        superUser.UserName = superEmail;
        superUser.Email = superEmail;
        superUser.EmailConfirmed = true;

        await userManager.CreateAsync(superUser, superPassword);

        await userManager.AddToRoleAsync(superUser, "Admin");
    }

}

// Creating default assistant account in DB
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();  // accessing UserManager services

    string assistEmail = "assist@assist.com";
    string assistPassword = "Assist2023!@#";

    if (await userManager.FindByEmailAsync(assistEmail) == null)
    {
        var assistUser = new IdentityUser();
        assistUser.UserName = assistEmail;
        assistUser.Email = assistEmail;
        assistUser.EmailConfirmed = true;

        await userManager.CreateAsync(assistUser, assistPassword);

        await userManager.AddToRoleAsync(assistUser, "Assistant");
    }

}

app.Run();
