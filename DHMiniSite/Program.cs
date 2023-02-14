using DHMiniSite.Models;
using DHMiniSite.Operations;
using DHMiniSite.Operations.Interfaces;
using DHRabbitMQCore.Abstract;
using DHRabbitMQCore.Concrete;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#else
    builder.Services.AddRazorPages();
#endif

builder.Services.Configure<DHMiniSiteDatabaseSettings>(
    builder.Configuration.GetSection("DHMiniSiteDatabase"));

builder.Services.AddScoped<IPostOperations,PostOperations>();
builder.Services.AddScoped<ICommentOperations, CommentOperations>();

builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
builder.Services.AddScoped<IRabbitMQConfiguration, RabbitMQConfiguration>();
builder.Services.AddScoped<IPublisherService, PublisherManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}");

});

app.Run();
