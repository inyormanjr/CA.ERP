using AutoMapper;
using CA.ERP.Domain.CustomerAgg;

namespace CA.ERP.DataAccess.AutoMapperProfiles
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, Entities.Customer>();
            CreateMap<Entities.Customer, Customer>();
        }
    }
}