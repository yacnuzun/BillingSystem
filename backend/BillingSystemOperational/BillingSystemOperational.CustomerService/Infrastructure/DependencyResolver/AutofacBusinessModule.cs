using Autofac;
using BillingSystemOperational.CustomerService.Application.Dto;
using BillingSystemOperational.CustomerService.Application.Service.Interface;
using BillingSystemOperational.CustomerService.Application.Validator;
using BillingSystemOperational.CustomerService.Infrastructure.Data;
using BillingSystemOperational.CustomerService.Infrastructure.Repository.Implementation;
using BillingSystemOperational.CustomerService.Infrastructure.Repository.Interface;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Persistance.Implementation;
using Shared.Persistance.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace BillingSystemOperational.CustomerService.Infrastructure.DependencyResolver
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region helper
            builder.RegisterType<EfUnitOfWork<CustomerDbContext>>().As<IUnitOfWork>();
            #endregion

            #region repo
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            #endregion

            #region service
            builder.RegisterType<Application.Service.Implementation.CustomerService>().As<ICustomerService>();
            #endregion

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<CustomerDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new CustomerDbContext(opts);
            })
            .AsSelf()
            .InstancePerLifetimeScope();


            #region validators
            builder.RegisterType<CustomerAddDtoValidator>().As<IValidator<CustomerAddDto>>().InstancePerLifetimeScope();
            #endregion

        }
    }
}
