using Ninject;
using AutoMapper;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Ninject;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.ProductServices
{
    public class BusinessLoanService : IProductService
    {
        private readonly IBusinessLoansService _businessLoansService;
        private readonly IBusinessLoansProduct _businessLoansProduct;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessLoansService"></param>
        /// <param name="businessLoansProduct"></param>
        public BusinessLoanService(IBusinessLoansService businessLoansService, IBusinessLoansProduct businessLoansProduct)
        {
            _businessLoansService = businessLoansService;
            _businessLoansProduct = businessLoansProduct;
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
                var loansRequest = mapper.Map<IBusinessLoansProduct, LoansRequest>(_businessLoansProduct);

                var result = _businessLoansService.SubmitApplicationFor(companyDataRequest, loansRequest);

                return (result.Success) ? result.ApplicationId ?? -1 : -1;
            }
        }
    }
}