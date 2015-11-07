namespace StudentSystem.Repositories.DataMappers
{
    using DbModels;
    using RequestModels;

    public interface IDataMapper
    {
        StudentRequestModel ToStudentRequestModel(Student student);

        Student ToStudentDatabaseModel(StudentRequestModel student);

        CourseRequestModel ToCourseRequestModel(Course course);

        Course ToCourseDatabaseModel(CourseRequestModel course);

        HomeworkRequestModel ToHomeworkRequestModel(Homework homework);

        Homework ToHomeworkDatabaseModel(HomeworkRequestModel homework);
    }
}