using AutoMapper;
using CA.ERP.Domain.CustomerAgg;

namespace CA.ERP.WebApp.Mapping
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, Dto.Customer.CustomerView>();
            CreateMap<Dto.Customer.CustomerCreate, Customer>();
            CreateMap<Dto.Customer.CustomerUpdate, Customer>();
        }
    }
}