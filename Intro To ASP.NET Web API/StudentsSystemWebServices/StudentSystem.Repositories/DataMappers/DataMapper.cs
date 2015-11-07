namespace StudentSystem.Repositories.DataMappers
{
    using System.Linq;
    using System.Reflection;

    using DbModels;
    using RequestModels;

    public class DataMapper : IDataMapper
    {
        public StudentRequestModel ToStudentRequestModel(Student student)
        {
            var requestModel = new StudentRequestModel();
            this.ReflectionMapper(student, requestModel);

            foreach (Homework homework in student.Homeworks)
            {
                requestModel.Homeworks
                    .Add(this.ToHomeworkRequestModel(homework));
            }

            return requestModel;
        }

        public Student ToStudentDatabaseModel(StudentRequestModel student)
        {
            var databaseModel = new Student();
            this.ReflectionMapper(student, databaseModel);
            student.Id = databaseModel.Id;

            return databaseModel;
        }

        public CourseRequestModel ToCourseRequestModel(Course course)
        {
            var requestModel = new CourseRequestModel();
            this.ReflectionMapper(course, requestModel);

            foreach (Student student in course.StudentsInCourse)
            {
                requestModel.EnlistedStudents
                    .Add(this.ToStudentRequestModel(student));
            }

            foreach (Homework homework in course.Homeworks)
            {
                requestModel.Homeworks
                    .Add(this.ToHomeworkRequestModel(homework));
            }

            return requestModel;
        }

        public Course ToCourseDatabaseModel(CourseRequestModel course)
        {
            var databaseModel = new Course();
            this.ReflectionMapper(course, databaseModel);

            foreach (StudentRequestModel student in course.EnlistedStudents)
            {
                databaseModel.StudentsInCourse
                    .Add(this.ToStudentDatabaseModel(student));
            }

            foreach (HomeworkRequestModel homework in course.Homeworks)
            {
                databaseModel.Homeworks
                    .Add(this.ToHomeworkDatabaseModel(homework));
            }

            course.Id = databaseModel.Id;
            return databaseModel;
        }

        public HomeworkRequestModel ToHomeworkRequestModel(Homework homework)
        {
            var requestModel = new HomeworkRequestModel();
            this.ReflectionMapper(homework, requestModel);

            return requestModel;
        }

        public Homework ToHomeworkDatabaseModel(HomeworkRequestModel homework)
        {
            var databaseModel = new Homework();
            this.ReflectionMapper(homework, databaseModel);

            if (homework.Student != null)
            {
                databaseModel.Student = this.ToStudentDatabaseModel(homework.Student);
            }

            if (homework.Course != null)
            {
                databaseModel.Course = this.ToCourseDatabaseModel(homework.Course);
            }

            homework.Id = databaseModel.Id;
            return databaseModel;
        }

        private void ReflectionMapper<T, TResult>(T model, TResult mapToModel)
            where T : class
            where TResult : class
        {
            var modelProperties = model.GetType().GetProperties();
            var mapToProperties = mapToModel.GetType().GetProperties();

            foreach (PropertyInfo mappedProperty in mapToProperties)
            {
                if (mappedProperty.PropertyType.IsValueType ||
                    mappedProperty.PropertyType.IsEquivalentTo(typeof(string)))
                {
                    var value = modelProperties
                        .First(prop => prop.Name.Equals(mappedProperty.Name))
                        .GetValue(model);

                    mappedProperty.SetValue(mapToModel, value);
                }
            }
        }
    }
}