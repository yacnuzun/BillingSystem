using Autofac.Extensions.DependencyInjection;
using Autofac;
using BillingSystem.AccountService.Infrastructure.DependencyResolver.Autofac;
using Microsoft.AspNetCore.Builder;
using BillingSystem.AccountService.Infrastructure.Helpers.JWT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configurationManager = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();   
var tokenOptions = configurationManager.GetSection("TokenOptions").Get<TokenOptions>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
