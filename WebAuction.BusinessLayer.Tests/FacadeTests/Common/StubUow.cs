using System.Threading.Tasks;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Tests.FacadeTests.Common
{
    public class StubUow : UnitOfWorkBase
    {
        public override void Dispose()
        {
        }

        protected override Task CommitCore()
        {
            return Task.Delay(10);
        }
    }
}
