using System.Threading.Tasks;

namespace vega.Db
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}