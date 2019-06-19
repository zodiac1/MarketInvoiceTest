using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.ProductServices
{
    public class SelectedInvoiceDiscountService : IProductService
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly ISelectiveInvoiceDiscountProduct _selectiveInvoiceDiscountProduct;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectInvoiceService"></param>
        /// <param name="selectiveInvoiceDiscountProduct"></param>
        public SelectedInvoiceDiscountService(ISelectInvoiceService selectInvoiceService, ISelectiveInvoiceDiscountProduct selectiveInvoiceDiscountProduct)
        {
            _selectInvoiceService = selectInvoiceService;
            _selectiveInvoiceDiscountProduct = selectiveInvoiceDiscountProduct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyData"></param>
        /// <returns></returns>
		public int SubmitApplication(ISellerCompanyData companyData)
        {
            return _selectInvoiceService.SubmitApplicationFor(companyData.Number.ToString(), _selectiveInvoiceDiscountProduct.InvoiceAmount, _selectiveInvoiceDiscountProduct.AdvancePercentage);
        }
    }
}