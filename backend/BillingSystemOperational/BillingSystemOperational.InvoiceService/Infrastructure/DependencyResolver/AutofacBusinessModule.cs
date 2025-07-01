using Autofac;
using BillingSystemOperational.InvoiceService.Application.Dto;
using BillingSystemOperational.InvoiceService.Application.Interface;
using BillingSystemOperational.InvoiceService.Application.Validator;
using BillingSystemOperational.InvoiceService.Infrastructure.Data;
using BillingSystemOperational.InvoiceService.Infrastructure.HttpClient;
using BillingSystemOperational.InvoiceService.Infrastructure.Repository.Implementation;
using BillingSystemOperational.InvoiceService.Infrastructure.Repository.Interface;
using FluentValidation;
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


            #region validator
            builder.RegisterType<InvoiceDeleteRequestDtoValidator>().As<IValidator<InvoiceDeleteRequestDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceLineSaveDtoValidator>().As<IValidator<InvoiceLineSaveDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceListItemRequestDtoValidator>().As<IValidator<InvoiceListItemRequestDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceSaveDtoValidator>().As<IValidator<InvoiceSaveDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceUpdateDtoValidator>().As<IValidator<InvoiceUpdateDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceLineUpdateDtoValidator>().As<IValidator<InvoiceLineUpdateDto>>().InstancePerLifetimeScope();
            #endregion

        }
    }
}
