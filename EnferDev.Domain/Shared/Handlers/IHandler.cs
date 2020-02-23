using EnferDev.Domain.Shared.Commands;
using System.Threading.Tasks;

namespace EnferDev.Domain.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handler(T command);
    }
}
