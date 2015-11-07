namespace StudentSystem.Repositories.RequestModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseRequestModel
    {
        public CourseRequestModel()
        {
            this.EnlistedStudents = new List<StudentRequestModel>();
            this.Homeworks = new List<HomeworkRequestModel>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Materials { get; set; }

        public IList<StudentRequestModel> EnlistedStudents { get; set; }

        public IList<HomeworkRequestModel> Homeworks { get; set; }
    }
}