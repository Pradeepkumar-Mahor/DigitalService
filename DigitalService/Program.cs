using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DeviceDetectorNET;
using DeviceDetectorNET.Cache;
using DigitalService.Domain.DbContext;
using DigitalService.Middleware.Extensions;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PushNotifications.Server.AspNetCore;
using WebEssentials.AspNetCore.Pwa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
 .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddProgressiveWebApp(new PwaOptions
{
    RegisterServiceWorker = true,
    RegisterWebmanifest = false,  // (Manually register in Layout file)
    Strategy = ServiceWorkerStrategy.NetworkFirst,
    OfflineRoute = "Offline.html"
});

builder.Services.AddPushNotifications();
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
    config.HasRippleEffect = true;
});
builder.Services.AddRazorPages();

var app = builder.Build();

//app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var detector = new DeviceDetector(context.Request.Headers["User-Agent"].ToString());
    detector.SetCache(new DictionaryCache());
    detector.Parse();

    if (detector.IsMobile())
    {
        context.Items.Remove("isMobile");
        context.Items.Add("isMobile", true);
    }
    else
    {
        context.Items.Remove("isMobile");
        context.Items.Add("isMobile", false);
    }

    context.Items.Remove("DeviceName");
    context.Items.Add("DeviceName", detector.GetDeviceName());

    await next();
});
app.UseNotyf();

//app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();
app.MapStaticAssets();

// Add custom middleware
app.UseUserRoleMiddleware();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//    endpoints.MapRazorPages();
//});

//app.MapRazorPages()
//   .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();