namespace StudentSystem.DbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Homework
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime? TimeSent { get; set; }

        public virtual Student Student { get; set; }

        public int? StudentId { get; set; }

        public virtual Course Course { get; set; }

        public int? CourseId { get; set; }
    }
}
