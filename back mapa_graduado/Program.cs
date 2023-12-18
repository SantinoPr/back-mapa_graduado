using back_mapa_graduado.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                                           .AllowAnyHeader()
                                           .AllowAnyMethod();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration
                      .GetConnectionString("ConnectionString")
                      ?? throw new ArgumentNullException("No tiene cadena conexion");

var postgreSQLConnectionConfiguration = new PostgreSQLConfiguration(connectionString);



builder.Services.AddSingleton(postgreSQLConnectionConfiguration);

builder.Services.AddScoped<IGraduadoDataAcces, GraduadoDataAcces>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    option.SlidingExpiration = true;
    option.Cookie.HttpOnly= true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles();

app.UseStaticFiles();

app.UseCors();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
