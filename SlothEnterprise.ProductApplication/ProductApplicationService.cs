using System;
using log4net;
using System.Reflection;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Mediator;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication
{
    public interface IProductApplicationService
    {
        IServicesMediator ServicesMediator { get; set; }
        int SubmitApplicationFor(ISellerApplication application);
    }

    public class ProductApplicationService : IProductApplicationService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IServicesMediator ServicesMediator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectInvoiceService"></param>
        /// <param name="confidentialInvoiceWebService"></param>
        /// <param name="businessLoansService"></param>
        public ProductApplicationService(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            ServicesMediator = new ServicesMediator();

            ServicesMediator.Add(typeof(ISelectInvoiceService), selectInvoiceService);
            ServicesMediator.Add(typeof(IConfidentialInvoiceService), confidentialInvoiceWebService);
            ServicesMediator.Add(typeof(IBusinessLoansService), businessLoansService);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public int SubmitApplicationFor(ISellerApplication application)
        {
            try
            {
                if (application != null && application.Product != null && application.CompanyData != null && ServicesMediator != null)
                {
                    if (application.Product is ProductServices.IServiceProvider)
                    {
                        ProductServices.IServiceProvider serviceProvider =
                            application.Product as ProductServices.IServiceProvider;

                        ProductServices.IProductService productService = serviceProvider.GetProductService(ServicesMediator);

                        return productService.SubmitApplication(application.CompanyData);
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw;
            }
        }
    }
}