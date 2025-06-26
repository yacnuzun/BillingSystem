using Autofac;
using BillingSystem.CustomerService.Application.Service.Interface;
using BillingSystem.CustomerService.Infrastructure.Data;
using BillingSystem.CustomerService.Infrastructure.Repository.Implementation;
using BillingSystem.CustomerService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Persistance.Implementation;
using BillingSystem.Shared.Persistance.Interface;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.CustomerService.Infrastructure.DependencyResolver
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




        }
    }
}
