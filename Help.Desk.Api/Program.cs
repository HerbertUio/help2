using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Ioc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        b => b
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed((hosts) => true));
});

builder.Services.RegisterDatabase(builder.Configuration).RegisterRepositories().RegisterServices().RegisterValidators();

var app = builder.Build();
app.UseCors("CORSPolicy");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIS HelpDesk V.1.0");
    c.RoutePrefix = "swagger";
    c.EnableFilter();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();
app.Run();