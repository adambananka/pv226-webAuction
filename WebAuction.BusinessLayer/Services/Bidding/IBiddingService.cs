using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.BusinessLayer.Services.Bidding
{
    public interface IBiddingService
    {
        Task PlaceBid(BidDto bidDto);
    }
}
