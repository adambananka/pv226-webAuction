using WebAuction.BusinessLayer.Facades.Common;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Facades
{
    // todo - pracovny nazov - chcelo by to lepsie pomenovanie
    public class UserInteractionFacade : FacadeBase
    {
        public UserInteractionFacade(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider) {}

        // todo - commenty a ratingy - metody na to
    }
}
