using CleanArchitecture.Application;
using CleanArchitecture.Identity;
using CleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Services.AddCors(
    options => 
        options.AddPolicy("CorsPolicy", 
            build => 
                build
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                )
);

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

// Add routing
app.UseRouting();

// Authentications
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

// Map controllers
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();