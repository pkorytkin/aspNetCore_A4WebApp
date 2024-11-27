namespace A4WebApp.Interfaces
{
    public interface IRepository<TBase,TKey>
    {
        Task<TBase> Get(TKey id);
        Task<List<TBase>> GetAll();

        Task Add(TBase entity);

        Task<bool> Delete(TKey id);

        Task Update(TBase item);

        Task<bool> Exists(TKey id);
    }
}
