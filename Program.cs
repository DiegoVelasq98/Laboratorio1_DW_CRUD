using Microsoft.EntityFrameworkCore;
using HelloApi4.Data;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Falta ConnectionStrings:DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseMySql(cs, ServerVersion.AutoDetect(cs)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migración automática en desarrollo (puedes dejarlo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
await app.RunAsync();
