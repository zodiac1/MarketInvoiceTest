using Ninject;
using AutoMapper;
using Ninject.Modules;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.AutoMapper;
using SlothEnterprise.ProductApplication.Mediator;

namespace SlothEnterprise.ProductApplication.Ninject
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = new Mappings().CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));

            Bind<IMappings>().To<Mappings>().InSingletonScope();
            Bind<IServicesMediator>().To<ServicesMediator>().InSingletonScope();

            Bind<ISellerApplication>().To<SellerApplication>();
            Bind<ISellerCompanyData>().To<SellerCompanyData>();
            Bind<IBusinessLoansProduct>().To<BusinessLoans>();
            Bind<IConfidentialInvoiceDiscountProduct>().To<ConfidentialInvoiceDiscount>();
            Bind<ISelectiveInvoiceDiscountProduct>().To<SelectiveInvoiceDiscount>();
        }
    }
}