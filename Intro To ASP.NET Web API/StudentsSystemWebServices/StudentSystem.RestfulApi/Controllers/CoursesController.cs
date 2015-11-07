namespace StudentSystem.RestfulApi.Controllers
{
    using Repositories;
    using Repositories.RepositoryInterfaces;
    using Repositories.RequestModels;

    public class CoursesController : BasicCrudController<CourseRequestModel>
    {
        public CoursesController() : this(new CoursesRepository())
        {
        }

        public CoursesController(IRepositoryBasic<CourseRequestModel> repository)
        {
            this.Repository = repository;
        }

        protected override IRepositoryBasic<CourseRequestModel> Repository { get; }
    }
}