using Autofac;
using BillingSystem.AccountService.Applicaiton.Service.Implementation;
using BillingSystem.AccountService.Applicaiton.Service.Interface;
using BillingSystem.AccountService.Applicaiton;
using BillingSystem.AccountService.Infrastructure.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BillingSystem.AccountService.Infrastructure.Helpers.JWT;
using BillingSystem.Shared.Persistance.Implementation;
using BillingSystem.Shared.Persistance.Interface;
using BillingSystem.AccountService.Infrastructure.Repository.Implementation;
using BillingSystem.AccountService.Infrastructure.Repository.Interface;
using BillingSystem.AccountService.Applicaiton.Validator;
using FluentValidation;

namespace BillingSystem.AccountService.Infrastructure.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region helper
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<EfUnitOfWork<AccountDbContext>>().As<IUnitOfWork>();
            #endregion
            #region repo
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            #endregion
            #region service
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            #endregion

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<AccountDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new AccountDbContext(opts);
            })
            .AsSelf()
            .InstancePerLifetimeScope();


            #region validators
            builder.RegisterType<UserForRegisterDtoValidator>().As<IValidator<UserForRegisterDto>>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerAddDtoValidator>().As<IValidator<CustomerAddDto>>().InstancePerLifetimeScope();
            #endregion

        }
    }
}
