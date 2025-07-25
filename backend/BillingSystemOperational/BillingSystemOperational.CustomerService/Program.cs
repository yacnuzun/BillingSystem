using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Helper.Encryption;
using BillingSystemOperational.CustomerService.Infrastructure.DependencyResolver;
using Shared.Helper.Security;
using BillingSystemOperational.CustomerService.Infrastructure.HttpClients;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configurationManager = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
var tokenOptions = configurationManager.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidIssuer = tokenOptions.Issuer,
                                    ValidAudience = tokenOptions.Audience,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
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
builder.Services.AddCors(
    opt =>
    {
        opt.AddPolicy("Frontend",
            p => p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
    }
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<TokenInjectionHandler>();
app.UseCors("Frontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
