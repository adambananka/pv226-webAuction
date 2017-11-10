using System;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure.Query;
using WebAuction.Infrastructure.Query.Predicates;
using WebAuction.Infrastructure.Query.Predicates.Operators;

namespace WebAuction.BusinessLayer.QueryObjects
{
    public class PaymentQueryObject : QueryObjectBase<PaymentDto, Payment, PaymentFilterDto, IQuery<Payment>>
    {
        public PaymentQueryObject(IMapper mapper, IQuery<Payment> query) : base(mapper, query)
        {
        }

        protected override IQuery<Payment> ApplyWhereClause(IQuery<Payment> query, PaymentFilterDto filter)
        {
            return filter.AuctionId.Equals(Guid.Empty)
                ? query
                : query.Where(new SimplePredicate(nameof(Payment.AuctionId), ValueComparingOperator.Equal,
                    filter.AuctionId));
        }
    }
}
