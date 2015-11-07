namespace StudentSystem.Repositories
{
    using DataLink;
    using DataMappers;
    using DbModels;
    using RequestModels;

    public class CoursesRepository : BaseRepository<StudentsDbContext, Course, CourseRequestModel>
    {
        public CoursesRepository()
            : this(new StudentsDbContext(), new MediatorMapper<Course, CourseRequestModel>(new DataMapper()))
        {
        }

        public CoursesRepository(
            StudentsDbContext dbContext,
            IMediatorMapper<Course, CourseRequestModel> dataMapper,
            int maxTransactionsForInstance = 50)
            : base(dbContext, dataMapper, maxTransactionsForInstance)
        {
        }
    }
}
