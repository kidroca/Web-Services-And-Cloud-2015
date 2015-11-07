namespace StudentSystem.DbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        private ICollection<Course> courses;

        private ICollection<Homework> homeworks;

        public Student()
        {
            this.courses = new HashSet<Course>();
            this.homeworks = new HashSet<Homework>();

            this.EducationForm = 0;
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

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }

            set { this.courses = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }

            set { this.homeworks = value; }
        }

        public override string ToString()
        {
            string str = string.Format(
                "I am {0} {1}, I go to {2} courses and I have completed {3} homeworks, call me {4}",
                this.FirstName,
                this.LastName,
                this.Courses.Count,
                this.Homeworks.Count,
                this.PhoneNumber);

            return str;
        }
    }
}
