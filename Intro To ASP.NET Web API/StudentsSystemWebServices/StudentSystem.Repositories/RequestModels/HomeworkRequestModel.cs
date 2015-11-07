namespace StudentSystem.Repositories.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HomeworkRequestModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime? TimeSent { get; set; }

        public StudentRequestModel Student { get; set; }

        public CourseRequestModel Course { get; set; }
    }
}