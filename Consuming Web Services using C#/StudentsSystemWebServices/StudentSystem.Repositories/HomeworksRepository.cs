namespace StudentSystem.Repositories
{
    using DataLink;
    using DataMappers;
    using DbModels;
    using RequestModels;

    public class HomeworksRepository : BaseRepository<StudentsDbContext, Homework, HomeworkRequestModel>
    {
        public HomeworksRepository()
            : this(
                  new StudentsDbContext(),
                  new MediatorMapper<Homework, HomeworkRequestModel>(new DataMapper()))
        {
        }

        public HomeworksRepository(
            StudentsDbContext dbContext,
            IMediatorMapper<Homework, HomeworkRequestModel> dataMapper,
            int maxTransactionsForInstance = 50)
            : base(dbContext, dataMapper, maxTransactionsForInstance)
        {
        }
    }
}
