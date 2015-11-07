namespace StudentSystem.Repositories.RequestModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DbModels;

    public class StudentRequestModel
    {
        public StudentRequestModel()
        {
            this.Courses = new List<CourseRequestModel>();
            this.Homeworks = new List<HomeworkRequestModel>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public EducationForm? EducationForm { get; set; }

        public IList<CourseRequestModel> Courses { get; set; }

        public IList<HomeworkRequestModel> Homeworks { get; set; }
    }
}