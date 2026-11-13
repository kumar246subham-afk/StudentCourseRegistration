using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistration.Models
{
    /// <summary>
    /// Student Model as per Marwadi University .NET Technologies (01CE1523)
    /// Fields: Id, Name, Email, Course, Semester
    /// Data Annotations: [Required], [StringLength], [Range], [EmailAddress]
    /// </summary>
    public class Student
    {
        [Display(Name = "Student ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 60 characters.")]
        [Display(Name = "Student Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address (e.g., student@marwadiuniversity.ac.in).")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course selection is required.")]
        [StringLength(50, ErrorMessage = "Course name cannot exceed 50 characters.")]
        [Display(Name = "Enrolled Course")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Semester is required.")]
        [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
        [Display(Name = "Current Semester")]
        public int Semester { get; set; }

        [Display(Name = "Registration Timestamp")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
