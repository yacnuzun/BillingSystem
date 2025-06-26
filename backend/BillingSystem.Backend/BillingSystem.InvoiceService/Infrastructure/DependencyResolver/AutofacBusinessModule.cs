using Autofac;
using BillingSystem.InvoiceService.Application.Implementation;
using BillingSystem.InvoiceService.Application.Interface;
using BillingSystem.InvoiceService.Infrastructure.Data;
using BillingSystem.InvoiceService.Infrastructure.Repository.Implementation;
using BillingSystem.InvoiceService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Persistance.Implementation;
using BillingSystem.Shared.Persistance.Interface;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.InvoiceService.Infrastructure.DependencyResolver
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region helper
            builder.RegisterType<EfUnitOfWork<InvoiceDbContext>>().As<IUnitOfWork>();
            #endregion
            #region service
            builder.RegisterType<Application.Implementation.InvoiceService>().As<IInvoiceService>();
            #endregion
            #region repo
            builder.RegisterType<InvoiceRepository>().As<IInvoiceRepository>();
            builder.RegisterType<InvoiceLineRepository>().As<IInvoiceLineRepository>();
            #endregion

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<InvoiceDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new InvoiceDbContext(opts);
            })
            .AsSelf()
            .InstancePerLifetimeScope();




        }
    }
}
