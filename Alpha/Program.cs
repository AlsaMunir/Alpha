using Microsoft.EntityFrameworkCore;
using Alpha.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the DbContext with SQL Server.
builder.Services.AddDbContext<AlphaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure identity services for authentication and authorization.
builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<AlphaContext>();

// Add distributed memory cache for session storage.
builder.Services.AddDistributedMemoryCache();

// Configure session options (ensure it's available for session usage).
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session timeout duration.
    options.Cookie.HttpOnly = true;  // Make the cookie accessible only by the server.
    options.Cookie.IsEssential = true;  // Mark the cookie as essential for the app.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Developer exception page for debugging.
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Custom error handling for production.
    app.UseHsts();  // HTTP Strict Transport Security (HSTS) for added security in production.
}

// Middleware for handling HTTPS redirection and static files.
app.UseHttpsRedirection();
app.UseStaticFiles();  // Serves static files like images, CSS, JS, etc.

// Add session middleware before routing (so that session can be accessed).
app.UseSession();

// Set up authentication and authorization middleware.
app.UseRouting();
app.UseAuthentication();  // Middleware to authenticate users.
app.UseAuthorization();   // Middleware to authorize users based on roles/permissions.

// Define route pattern for MVC controllers.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor Pages if you are using them in your app.
app.MapRazorPages();

// Run the app.
app.Run();
