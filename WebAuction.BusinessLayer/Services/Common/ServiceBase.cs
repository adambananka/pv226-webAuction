using AutoMapper;

namespace WebAuction.BusinessLayer.Services.Common
{
    public abstract class ServiceBase
    {
        protected readonly IMapper Mapper;

        protected ServiceBase(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
