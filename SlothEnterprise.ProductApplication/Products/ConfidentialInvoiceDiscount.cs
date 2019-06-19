using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Mediator;
using SlothEnterprise.ProductApplication.ProductServices;

namespace SlothEnterprise.ProductApplication.Products
{
    public interface IConfidentialInvoiceDiscountProduct
    {
        int Id { get; }
        decimal TotalLedgerNetworth { get; }
        decimal AdvancePercentage { get; }
        decimal VatRate { get; }
    }

    public class ConfidentialInvoiceDiscount : IProduct, IConfidentialInvoiceDiscountProduct, IServiceProvider
    {
        public int Id { get; set; }
        public decimal TotalLedgerNetworth { get; set; }
        public decimal AdvancePercentage { get; set; }
        public decimal VatRate { get; set; } = VatRates.UkVatRate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servicesMediator"></param>
        /// <returns></returns>
        public IProductService GetProductService(IServicesMediator servicesMediator)
        {
            return new ConfidentialInvoiceDiscountService(servicesMediator.Services[typeof(IConfidentialInvoiceService)] as IConfidentialInvoiceService, this);
        }
    }
}