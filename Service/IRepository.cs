namespace ReceivingService.Service
{
    public interface IRepository<T>
    {
         void Save(T item);
    }
}