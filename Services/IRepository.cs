using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReceivingService.Services
{
    public interface IRepository<T>
    {
         Task Save(T item);
         IEnumerable<T> GetAll();
    }
}