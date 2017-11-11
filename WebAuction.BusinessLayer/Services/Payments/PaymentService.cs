using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.Services.Payments
{
    public class PaymentService : CrudQueryServiceBase<Payment, PaymentDto, PaymentFilterDto>, IPaymentService
    {
        public PaymentService(IMapper mapper, IRepository<Payment> repository,
            QueryObjectBase<PaymentDto, Payment, PaymentFilterDto, IQuery<Payment>> query) : base(mapper, repository,
            query)
        {
        }

        protected override async Task<Payment> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<PaymentDto> GetPaymentAccordingToAuctionAsync(Guid auctionId)
        {
            var queryResult = await Query.ExecuteQuery(new PaymentFilterDto {AuctionId = auctionId});
            return queryResult.Items.SingleOrDefault();
        }
    }
}