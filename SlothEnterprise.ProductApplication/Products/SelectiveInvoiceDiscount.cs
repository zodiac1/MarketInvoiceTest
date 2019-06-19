using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Mediator;
using SlothEnterprise.ProductApplication.ProductServices;

namespace SlothEnterprise.ProductApplication.Products
{
    public interface ISelectiveInvoiceDiscountProduct
    {
        int Id { get; }
        decimal InvoiceAmount { get; }
        decimal AdvancePercentage { get; }
    }

    public class SelectiveInvoiceDiscount : IProduct, ISelectiveInvoiceDiscountProduct, IServiceProvider
    {
        public int Id { get; set; }
        /// <summary>
        /// Proposed networth of the Invoice
        /// </summary>
        public decimal InvoiceAmount { get; set; }
        /// <summary>
        /// Percentage of the networth agreed and advanced to seller
        /// </summary>
        public decimal AdvancePercentage { get; set; } = 0.80M;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicesMediator"></param>
        /// <returns></returns>
        public IProductService GetProductService(IServicesMediator servicesMediator)
        {
            return new SelectedInvoiceDiscountService(servicesMediator.Services[typeof(ISelectInvoiceService)] as ISelectInvoiceService, this);
        }
    }
}