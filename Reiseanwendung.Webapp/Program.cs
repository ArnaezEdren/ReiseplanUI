using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Infrastructure.Repositories;
using Reiseanwendung.Webapp.Dto;

var opt = new DbContextOptionsBuilder<ReiseplanContext>() // Specify the generic type parameter here
             .UseSqlite("Data Source=Reiseplan.db")
             .Options;
using (var db = new ReiseplanContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.SeedData(db);
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ReiseplanContext>(opt =>
{
    opt.UseSqlite("Data Source=Reiseplan.db");
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddRazorPages();
builder.Services.AddScoped<ReiseplanRepository>(); // Register the ReiseplanRepository

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();

app.Run();
