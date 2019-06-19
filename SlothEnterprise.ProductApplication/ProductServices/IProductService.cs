using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.ProductServices
{
    public interface IProductService
    {
        int SubmitApplication(ISellerCompanyData companyData);
    }
}