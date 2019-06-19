using AutoMapper;
using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.AutoMapper.Profiles;

namespace SlothEnterprise.ProductApplication.AutoMapper
{
    public interface IMappings
    {
        MapperConfiguration Config { get; set; }
    }

    public class Mappings : IMappings
    {
        public MapperConfiguration Config { get; set; }

        public MapperConfiguration CreateConfiguration()
        {
            if (Config == null)
            {
                Config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<GenericMappingProfile>();
                    cfg.CreateMap<ISellerCompanyData, CompanyDataRequest>();
                    cfg.CreateMap<IBusinessLoansProduct, LoansRequest>();
                });
            }

            return Config;
        }
    }
}