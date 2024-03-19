using AspCoreCRUDLayered.Business;
using AspCoreCRUDLayered.DAL;
using AspCoreCRUDLayered.DAL.DbModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBusiness, Business>();
builder.Services.AddScoped<IDataAccess, DataAccess>();

builder.Services.AddDbContext<SchoolDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
