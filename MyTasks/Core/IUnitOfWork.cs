using MyTasks.Core.Repositories;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Core
{
    public interface IUnitOfWork
    {
        ITaskRepository Task { get; }
        void Complete();
    }
}
