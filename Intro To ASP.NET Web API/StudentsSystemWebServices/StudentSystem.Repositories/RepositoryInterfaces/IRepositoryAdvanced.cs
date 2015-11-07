namespace StudentSystem.Repositories.RepositoryInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepositoryAdvanced<TRequsetModel> : IRepositoryBasic<TRequsetModel>
        where TRequsetModel : class
    {
        Task<IList<TRequsetModel>> GetObjectLike(TRequsetModel model);

        Task<IList<TRequsetModel>> GetByQueryString(params string[] queryString);

        Task<bool> Delete(TRequsetModel model);
    }
}