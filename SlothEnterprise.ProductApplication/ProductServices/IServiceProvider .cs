using SlothEnterprise.ProductApplication.Mediator;

namespace SlothEnterprise.ProductApplication.ProductServices
{
    public interface IServiceProvider
    {
        IProductService GetProductService(IServicesMediator servicesMediator);
    }
}