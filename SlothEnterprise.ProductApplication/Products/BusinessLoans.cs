using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Mediator;
using SlothEnterprise.ProductApplication.ProductServices;

namespace SlothEnterprise.ProductApplication.Products
{
    public interface IBusinessLoansProduct
    {
        int Id { get; }
        decimal InterestRatePerAnnum { get; }
        decimal LoanAmount { get; }
    }

    public class BusinessLoans : IProduct, IBusinessLoansProduct, IServiceProvider
    {
        public int Id { get; set; }
        /// <summary>
        /// Per annum interest rate
        /// </summary>
        public decimal InterestRatePerAnnum { get; set; }

        /// <summary>
        /// Total available amount to withdraw
        /// </summary>
        public decimal LoanAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicesMediator"></param>
        /// <returns></returns>
        public IProductService GetProductService(IServicesMediator servicesMediator)
        {
            return new BusinessLoanService(servicesMediator.Services[typeof(IBusinessLoansService)] as IBusinessLoansService, this);
        }
    }
}