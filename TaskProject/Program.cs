using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Numerics;
using TaskProject.Helpers;
using TaskProject.Repository.ProjectRepo;
using TaskProject.Repository.TaskRepo;
using TaskProject.Service.Project;
using TaskProject.Service.TaskServ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(
              options =>
              {
                  options.IdleTimeout = TimeSpan.FromMinutes(30);
              });//setting middleware


builder.Services.AddDbContext<Context>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
    }, ServiceLifetime.Scoped); 

//Register (userManager , RoleManager ,Stores)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    //Change the Default of Password by userManager
    Options =>
    {
        Options.Password.RequireNonAlphanumeric=false;
        Options.Password.RequiredLength = 5;
    }
    ).AddEntityFrameworkStores<Context>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



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
app.UseSession();//default setting "Service"
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
