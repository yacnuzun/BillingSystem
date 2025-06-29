using Autofac;
using BillingSystemOperational.InvoiceService.Application.Interface;
using BillingSystemOperational.InvoiceService.Infrastructure.Data;
using BillingSystemOperational.InvoiceService.Infrastructure.Repository.Implementation;
using BillingSystemOperational.InvoiceService.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Shared.Persistance.Implementation;
using Shared.Persistance.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace BillingSystemOperational.InvoiceService.Infrastructure.DependencyResolver
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
