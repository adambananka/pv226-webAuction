using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;

namespace WebAuction.BusinessLayer.Services.ClosingAuction
{
    public class ClosingAuctionService : ServiceBase, IClosingAuctionService
    {
        private readonly IRepository<Auction> _auctionRepository;
        public ClosingAuctionService(IMapper mapper, IRepository<Auction> auctionRepository) : base(mapper)
        {
            _auctionRepository = auctionRepository;
        }

        // todo - premysliet dto
        public void CloseAuctionDueToTimeout(AuctionDto auctionDto)
        {
            var auction = Mapper.Map<Auction>(auctionDto);

        }

        // todo - premysliet Dto
        public void CloseAuctionDueToBuyout(AuctionDto auctionDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
