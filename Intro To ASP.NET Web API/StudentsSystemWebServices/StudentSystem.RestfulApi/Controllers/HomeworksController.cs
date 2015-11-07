namespace StudentSystem.RestfulApi.Controllers
{
    using Repositories;
    using Repositories.RepositoryInterfaces;
    using Repositories.RequestModels;

    public class HomeworksController : BasicCrudController<HomeworkRequestModel>
    {
        public HomeworksController() : this(new HomeworksRepository())
        {
        }

        public HomeworksController(IRepositoryBasic<HomeworkRequestModel> repository)
        {
            this.Repository = repository;
        }

        protected override IRepositoryBasic<HomeworkRequestModel> Repository { get; }
    }
}