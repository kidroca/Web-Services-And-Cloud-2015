namespace StudentSystem.Repositories.DataMappers
{
    using System;
    using DbModels;
    using RequestModels;

    /// <summary>
    /// I know type checing is frowned upon, but this implementation saves me from writing 
    /// a lot of similar code
    /// </summary>
    /// <typeparam name="TDbModel">Database Model object</typeparam>
    /// <typeparam name="TRequestModel">Request Model object</typeparam>
    public class MediatorMapper<TDbModel, TRequestModel>
        : IMediatorMapper<TDbModel, TRequestModel>
        where TDbModel : class
        where TRequestModel : class
    {
        private readonly IDataMapper concreteMapper;

        public MediatorMapper(IDataMapper concreteMapper)
        {
            this.concreteMapper = concreteMapper;
        }

        public TDbModel ToDatabaseModel(TRequestModel requestModel)
        {
            var type = requestModel.GetType().BaseType;

            if (type.IsEquivalentTo(typeof(StudentRequestModel)))
            {
                return this.concreteMapper
                    .ToStudentDatabaseModel(requestModel as StudentRequestModel) as TDbModel;
            }
            else if (type.IsEquivalentTo(typeof(CourseRequestModel)))
            {
                return this.concreteMapper
                    .ToCourseDatabaseModel(requestModel as CourseRequestModel) as TDbModel;
            }
            else if (type.IsEquivalentTo(typeof(HomeworkRequestModel)))
            {
                return this.concreteMapper
                    .ToHomeworkDatabaseModel(requestModel as HomeworkRequestModel) as TDbModel;
            }
            else
            {
                throw new ArgumentException("Unable to match to any database model type");
            }
        }

        public TRequestModel ToRequestModel(TDbModel databaseModel)
        {
            var type = databaseModel.GetType().BaseType;

            if (type.IsEquivalentTo(typeof(Student)))
            {
                return this.concreteMapper
                    .ToStudentRequestModel(databaseModel as Student) as TRequestModel;
            }
            else if (type.IsEquivalentTo(typeof(Course)))
            {
                return this.concreteMapper
                    .ToCourseRequestModel(databaseModel as Course) as TRequestModel;
            }
            else if (type.IsEquivalentTo(typeof(Homework)))
            {
                return this.concreteMapper
                    .ToHomeworkRequestModel(databaseModel as Homework) as TRequestModel;
            }
            else
            {
                throw new ArgumentException("Unable to match to any request model type");
            }
        }
    }
}
