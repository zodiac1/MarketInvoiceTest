using Ninject;
using AutoMapper;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Ninject;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.ProductServices
{
    public class ConfidentialInvoiceDiscountService : IProductService
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceService;
        private readonly IConfidentialInvoiceDiscountProduct _confidentialInvoiceDiscountProduct;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="confidentialInvoiceService"></param>
        /// <param name="confidentialInvoiceDiscountProduct"></param>
        public ConfidentialInvoiceDiscountService(IConfidentialInvoiceService confidentialInvoiceService, IConfidentialInvoiceDiscountProduct confidentialInvoiceDiscountProduct)
        {
            _confidentialInvoiceService = confidentialInvoiceService;
            _confidentialInvoiceDiscountProduct = confidentialInvoiceDiscountProduct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyData"></param>
        /// <returns></returns>
        public int SubmitApplication(ISellerCompanyData companyData)
        {
            using (IKernel kernel = new StandardKernel(new NinjectBindings()))
            {
                IMapper mapper = kernel.Get<IMapper>();

                var companyDataRequest = mapper.Map<ISellerCompanyData, CompanyDataRequest>(companyData);
                var result = _confidentialInvoiceService.SubmitApplicationFor(companyDataRequest,
                    _confidentialInvoiceDiscountProduct.TotalLedgerNetworth, _confidentialInvoiceDiscountProduct.AdvancePercentage, _confidentialInvoiceDiscountProduct.VatRate);

                return (result.Success) ? result.ApplicationId ?? -1 : -1;
            }
        }
    }
}