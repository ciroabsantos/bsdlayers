using System.Threading.Tasks;

namespace BsdLayers.Business.Commands
{
    /// <summary>
    /// Interface for Business Commands
    /// </summary>
    public interface IBusinessCommand
    {
        Task<BusinessCommandResult> ExecuteAsync();

        Task<BusinessCommandResult> RollBackAsync();
    }

    /// <summary>
    /// Interface for commands that produces a content in the result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBusinessCommand<T> : IBusinessCommand
    {
        Task<BusinessCommandResult<T>> ExecuteForContentAsync();
    }
}
