using Moq;
using Xunit;
using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        [Fact]
        public void WhenSellerApplicationIsNullThrowsArgumentNullException()
        {
            IProductApplicationService applicationService = new ProductApplicationService(null, null, null);

            Assert.Throws<ArgumentNullException>(() => applicationService.SubmitApplicationFor(null));
        }

        [Fact]
        public void WhenProductIsSelectiveInvoiceCanHandleSubmitApplication()
        {
            //Arrange

            IProduct product = new SelectiveInvoiceDiscount { Id = 1, InvoiceAmount = 300, AdvancePercentage = 5 };

            ISellerCompanyData companyData1 = new SellerCompanyData { DirectorName = "Me", Founded = DateTime.Now, Name = "My Company", Number = 12 };
            ISellerCompanyData companyData2 = new SellerCompanyData { DirectorName = "Us", Founded = DateTime.Now, Name = "Our Company", Number = 10 };

            ISellerApplication sellerApp1 = new SellerApplication{Product = product, CompanyData = companyData1 };
            ISellerApplication sellerApp2 = new SellerApplication { Product = product, CompanyData = companyData2 };

            //-------------------------------------------------------------------------------------------------------

            Mock<ISelectInvoiceService> mockSelectInvoiceService = new Mock<ISelectInvoiceService>(MockBehavior.Strict);
            Mock<IConfidentialInvoiceService> mockConfidentialInvoiceService = new Mock<IConfidentialInvoiceService>(MockBehavior.Strict);
            Mock<IBusinessLoansService> mockBusinessLoansService = new Mock<IBusinessLoansService>(MockBehavior.Strict);

            mockSelectInvoiceService.Setup(m => m.SubmitApplicationFor(It.Is<string>(companyNumber => companyNumber == "12"), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(1);
            mockSelectInvoiceService.Setup(m => m.SubmitApplicationFor(It.Is<string>(companyNumber => companyNumber != "12"), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(-1);

            //-------------------------------------------------------------------------------------------------------

            IProductApplicationService applicationService = new ProductApplicationService(mockSelectInvoiceService.Object, mockConfidentialInvoiceService.Object, mockBusinessLoansService.Object);

            //Act
            int result1 = applicationService.SubmitApplicationFor(sellerApp1);
            int result2 = applicationService.SubmitApplicationFor(sellerApp2);

            //Assert
            mockSelectInvoiceService.Verify();

            Assert.Equal(1, result1);
            Assert.Equal(-1, result2);
        }

        [Fact]
        public void WhenProductIsConfidentialInvoiceDiscountCanHandleSubmitApplication()
        {
            //Arrange

            IProduct product = new ConfidentialInvoiceDiscount { Id = 2, AdvancePercentage = 3, TotalLedgerNetworth = 500, VatRate = 0.3M };

            ISellerCompanyData companyData1 = new SellerCompanyData { DirectorName = "Me", Founded = DateTime.Now, Name = "My Company", Number = 12 };
            ISellerCompanyData companyData2 = new SellerCompanyData { DirectorName = "Us", Founded = DateTime.Now, Name = "Our Company", Number = 10 };

            ISellerApplication sellerApp1 = new SellerApplication { Product = product, CompanyData = companyData1 };
            ISellerApplication sellerApp2 = new SellerApplication { Product = product, CompanyData = companyData2 };

            //-------------------------------------------------------------------------------------------------------

            Mock<ISelectInvoiceService> mockSelectInvoiceService = new Mock<ISelectInvoiceService>(MockBehavior.Strict);
            Mock<IConfidentialInvoiceService> mockConfidentialInvoiceService = new Mock<IConfidentialInvoiceService>(MockBehavior.Strict);
            Mock<IBusinessLoansService> mockBusinessLoansService = new Mock<IBusinessLoansService>(MockBehavior.Strict);

            Mock<IApplicationResult> appResultSuccess = new Mock<IApplicationResult>();
            Mock<IApplicationResult> appResultFail = new Mock<IApplicationResult>();

            appResultSuccess.SetupGet(x => x.ApplicationId).Returns(3);
            appResultSuccess.Setup(x => x.Success).Returns(true);

            appResultFail.Setup(x => x.Success).Returns(false);

            mockConfidentialInvoiceService.Setup(m => m.SubmitApplicationFor(It.Is<CompanyDataRequest>(r => r.CompanyName == "My Company"), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(appResultSuccess.Object);
            mockConfidentialInvoiceService.Setup(m => m.SubmitApplicationFor(It.Is<CompanyDataRequest>(r => r.CompanyName != "My Company"), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(appResultFail.Object);

            //-------------------------------------------------------------------------------------------------------

            IProductApplicationService applicationService = new ProductApplicationService(mockSelectInvoiceService.Object, mockConfidentialInvoiceService.Object, mockBusinessLoansService.Object);

            //Act
            int result1 = applicationService.SubmitApplicationFor(sellerApp1);
            int result2 = applicationService.SubmitApplicationFor(sellerApp2);

            //Assert
            mockConfidentialInvoiceService.Verify();

            Assert.Equal(3, result1);
            Assert.Equal(-1, result2);
        }

        [Fact]
        public void WhenProductIsBusinessLoansCanHandleSubmitApplication()
        {
            //Arrange

            IProduct product = new BusinessLoans{ Id = 3, InterestRatePerAnnum = 0.6M, LoanAmount = 60};

            ISellerCompanyData companyData1 = new SellerCompanyData { DirectorName = "Me", Founded = DateTime.Now, Name = "My Company", Number = 12 };
            ISellerCompanyData companyData2 = new SellerCompanyData { DirectorName = "Us", Founded = DateTime.Now, Name = "Our Company", Number = 10 };

            ISellerApplication sellerApp1 = new SellerApplication { Product = product, CompanyData = companyData1 };
            ISellerApplication sellerApp2 = new SellerApplication { Product = product, CompanyData = companyData2 };

            //-------------------------------------------------------------------------------------------------------

            Mock<ISelectInvoiceService> mockSelectInvoiceService = new Mock<ISelectInvoiceService>(MockBehavior.Strict);
            Mock<IConfidentialInvoiceService> mockConfidentialInvoiceService = new Mock<IConfidentialInvoiceService>(MockBehavior.Strict);
            Mock<IBusinessLoansService> mockBusinessLoansService = new Mock<IBusinessLoansService>(MockBehavior.Strict);

            Mock<IApplicationResult> appResultSuccess = new Mock<IApplicationResult>();
            Mock<IApplicationResult> appResultFail = new Mock<IApplicationResult>();

            appResultSuccess.SetupGet(x => x.ApplicationId).Returns(3);
            appResultSuccess.Setup(x => x.Success).Returns(true);

            appResultFail.Setup(x => x.Success).Returns(false);

            mockBusinessLoansService.Setup(m => m.SubmitApplicationFor(It.Is<CompanyDataRequest>(r => r.CompanyName == "My Company"), It.IsAny<LoansRequest>())).Returns(appResultSuccess.Object);
            mockBusinessLoansService.Setup(m => m.SubmitApplicationFor(It.Is<CompanyDataRequest>(r => r.CompanyName != "My Company"), It.IsAny<LoansRequest>())).Returns(appResultFail.Object);

            //-------------------------------------------------------------------------------------------------------

            IProductApplicationService applicationService = new ProductApplicationService(mockSelectInvoiceService.Object, mockConfidentialInvoiceService.Object, mockBusinessLoansService.Object);

            //Act
            int result1 = applicationService.SubmitApplicationFor(sellerApp1);
            int result2 = applicationService.SubmitApplicationFor(sellerApp2);

            //Assert
            mockBusinessLoansService.Verify();

            Assert.Equal(3, result1);
            Assert.Equal(-1, result2);
        }
    }
}
