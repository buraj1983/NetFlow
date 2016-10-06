using System;

namespace NetFlow.Infrastructure.Processes
{
    public interface IProcessManagerDataContext<TProcessManager> where TProcessManager : IProcessManager
    {
        TProcessManager Find(Guid id);

        void Save(TProcessManager processManager);
    }
}