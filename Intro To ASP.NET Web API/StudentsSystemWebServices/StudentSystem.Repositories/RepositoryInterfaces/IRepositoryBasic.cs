namespace StudentSystem.Repositories.RepositoryInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepositoryBasic<TRequsetModel> where TRequsetModel : class
    {
        Task<TRequsetModel> GetById(int id);

        Task<IList<TRequsetModel>> GetAll();

        Task<bool> Update(int id, TRequsetModel model);

        Task<int> Create(TRequsetModel model);

        Task<bool> Delete(int id);
    }
}
