using AutoMapper;
using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.AutoMapper.Profiles
{
    public class GenericMappingProfile : Profile
    {
        public GenericMappingProfile()
        {
            var companyDataMap = CreateMap<ISellerCompanyData, CompanyDataRequest>();
            companyDataMap.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name));
            companyDataMap.ForMember(dest => dest.CompanyNumber, opt => opt.MapFrom(src => src.Number));
            companyDataMap.ForMember(dest => dest.CompanyFounded, opt => opt.MapFrom(src => src.Founded));

            CreateMap<IBusinessLoansProduct, LoansRequest>();
        }
    }
}