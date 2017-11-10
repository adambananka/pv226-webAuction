using WebAuction.BusinessLayer.Facades.Common;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Facades
{
    // todo - pracovny nazov - zmenit na nieco vhodnejsie
    public class AuctionProcess : FacadeBase
    {
        public AuctionProcess(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider) {}

        // todo - prihodit bid, 
        // 1. kupit item na aukcii hned - buyout
        // 2. kupit item ako posledny bidder a ked skonci aukcia
    }
}
