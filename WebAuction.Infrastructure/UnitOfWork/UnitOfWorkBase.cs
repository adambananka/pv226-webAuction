using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAuction.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private readonly List<Action> _actionsAfterCommit = new List<Action>();

        public async Task Commit()
        {
            await CommitCore();
            foreach (var action in _actionsAfterCommit)
            {
                action();
            }

            _actionsAfterCommit.Clear();
        }

        public void RegisterAction(Action action)
        {
            _actionsAfterCommit.Add(action);
        }

        public abstract void Dispose();

        protected abstract Task CommitCore();
    }
}