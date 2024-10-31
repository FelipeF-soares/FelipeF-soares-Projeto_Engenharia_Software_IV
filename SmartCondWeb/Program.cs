using Microsoft.EntityFrameworkCore;
using SmartCondWeb.DataAcess.ContextPersist;
using SmartCondWeb.DataAcess.Persist;
using SmartCondWeb.DataAcess.Persist.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var stringConnection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SmartCondContext>
    (
        options => options.UseMySql(stringConnection,ServerVersion.AutoDetect(stringConnection))
    );
builder.Services.AddScoped<IHomeownerPersist,HomeownerPersist>();
builder.Services.AddScoped<IPetPersist,PetPersist>();
builder.Services.AddScoped<IResidentPersist,ResidentPersist>();
builder.Services.AddScoped<IUnitPersist,UnitPersist>();
builder.Services.AddScoped<IVehiclePersist,VehiclePersist>();

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
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
