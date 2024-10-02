using api.src.Data;
using api.src.Interfaces;
using api.src.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite("Data Source=catedra1.db");
});
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
var app = builder.Build();


using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seeder.Seed(context);
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

