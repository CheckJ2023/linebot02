using linebot02.Startup;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.RegisterServices();

var app = builder.Build();


// Configure the HTTP request pipeline

app.ConfigurationSwagger();

// app.UseHttpsRedirection();



app.MapAllEndpoints();

app.Run();

