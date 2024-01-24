using SocialHub.Server.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SocialHub.Server.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services are added to ApplicationServiceExtension.cs and called here
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

//app.UseDefaultFiles();
//app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

//"using" statement is used to dispose the scope after the code is executed
//This is a manual way of garbage collection
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}


app.Run();