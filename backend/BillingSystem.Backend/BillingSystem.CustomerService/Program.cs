using Autofac.Extensions.DependencyInjection;
using Autofac;
using BillingSystem.CustomerService.Infrastructure.DependencyResolver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BillingSystem.Shared.Helper.Security.Security;
using BillingSystem.Shared.Helper.Security.Encryption;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configurationManager = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
var tokenOptions = configurationManager.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = false,
                                    ValidIssuer = "yacn.uzun@gmail.com",
                                    ValidAudience = "yacn.uzun@gmail.com",
                                    ValidateIssuerSigningKey = false,
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey("your_very_long_secret_key_here_at_least_64_characters_long")
                                };
                                options.Events = new JwtBearerEvents
                                {
                                    OnAuthenticationFailed = context =>
                                    {
                                        Console.WriteLine($"JWT Auth failed: {context.Exception.Message}");
                                        return Task.CompletedTask;
                                    },
                                    OnChallenge = context =>
                                    {
                                        Console.WriteLine($"JWT Challenge error: {context.ErrorDescription}");
                                        return Task.CompletedTask;
                                    }
                                };
                            });
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
