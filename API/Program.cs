using API.Data;
using API.Entities;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<Storecontext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddIdentityApiEndpoints<User>(opt=>
{
    opt.User.RequireUniqueEmail= true;
})

     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<Storecontext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware <ExceptionMiddleware>();
app.UseCors(opt =>
{
 opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:3000");
}
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<User>(); //api//login

 await Dbinitializer.InitDb(app);

app.Run();
