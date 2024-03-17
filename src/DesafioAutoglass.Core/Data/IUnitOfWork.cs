using System.Threading.Tasks;

namespace DesafioAutoglass.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
