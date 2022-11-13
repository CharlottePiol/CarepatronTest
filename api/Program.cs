using api.Data;
using api.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using api.Models;
using static Microsoft.AspNetCore.Http.Results;
using FluentValidation.Validators;
using api.Validator;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<IValidator<Client>, ClientValidator>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var services = builder.Services;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// cors
services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .Build());
});

// ioc
services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "Test"));

services.AddScoped<DataSeeder>();
services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/clients", async (IClientRepository clientRepository) =>
{
    return await clientRepository.Get();
}).WithName("get clients");


app.MapGet("/clients/filter={filter}",  async (string filter, IClientRepository clientRepository) =>
{
     return  await clientRepository.GetByFilter(filter);
}).WithName("get clients by filter");


app.MapPost("/clients", async (IValidator<Client> validator, IClientRepository repository, Client client) =>
{
    var validationResult = await validator.ValidateAsync(client);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    await repository.Create(client);
    return Results.Created($"/{client.Id}", client);
}).WithName("create client");


app.MapPut("/clients/{id}", async (IValidator<Client> validator, IClientRepository repository, Client client) =>
{
    var validationResult = await validator.ValidateAsync(client);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    await repository.Update(client);
    return Results.Created($"/{client.Id}", client);
}).WithName("update client");

app.UseCors();

// seed data
using (var scope = app.Services.CreateScope())
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

    dataSeeder.Seed();
}

// run app
app.Run();
