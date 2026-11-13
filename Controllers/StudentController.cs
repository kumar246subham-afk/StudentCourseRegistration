using Microsoft.AspNetCore.Mvc;
using StudentCourseRegistration.Models;

namespace StudentCourseRegistration.Controllers
{
    /// <summary>
    /// StudentController handling Course Registration workflows
    /// Complies with Marwadi University .NET Technologies (01CE1523) Activity Specification:
    /// - Index() : Displays Student List
    /// - Create() GET : Returns Student Registration Form
    /// - Create() POST : Model Binding & Validation via ModelState.IsValid
    /// - Details(int id) : Displays individual Student Details
    /// </summary>
    public class StudentController : Controller
    {
        // Thread-safe in-memory data store for registrations
        private static readonly List<Student> _students = new()
        {
            new Student
            {
                Id = 1,
                Name = "Subham Kumar",
                Email = "subham.kumar@marwadiuniversity.ac.in",
                Course = "B.Tech Computer Engineering",
                Semester = 5,
                RegistrationDate = DateTime.Now.AddDays(-5)
            },
            new Student
            {
                Id = 2,
                Name = "Priya Sharma",
                Email = "priya.sharma@marwadiuniversity.ac.in",
                Course = "B.Tech Information Technology",
                Semester = 5,
                RegistrationDate = DateTime.Now.AddDays(-3)
            },
            new Student
            {
                Id = 3,
                Name = "Rahul Patel",
                Email = "rahul.patel@marwadiuniversity.ac.in",
                Course = "B.Tech Computer Engineering",
                Semester = 3,
                RegistrationDate = DateTime.Now.AddDays(-1)
            }
        };

        // Available Courses for dropdown binding
        public static readonly List<string> AvailableCourses = new()
        {
            "B.Tech Computer Engineering",
            "B.Tech Information Technology",
            "B.Tech Artificial Intelligence & Data Science",
            "B.Tech Mechanical Engineering",
            "B.Tech Electrical Engineering",
            "B.Tech Civil Engineering"
        };

        // ============================================================
        // GET: /Student/Index  --  Student List Page
        // ============================================================
        public IActionResult Index()
        {
            // Passes in-memory list from Controller to View
            return View(_students.OrderByDescending(s => s.Id).ToList());
        }

        // ============================================================
        // GET: /Student/Create  --  Registration Form
        // ============================================================
        public IActionResult Create()
        {
            ViewBag.Courses = AvailableCourses;
            return View(new Student { Semester = 5 });
        }

        // ============================================================
        // POST: /Student/Create  --  Handle Form Submission
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            // Server-side Validation using Data Annotations
            if (!ModelState.IsValid)
            {
                // Invalid: Return the same view with input values and error messages
                ViewBag.Courses = AvailableCourses;
                return View(student);
            }

            // Valid: Assign auto-increment ID and persist to list
            lock (_students)
            {
                student.Id = _students.Count > 0 ? _students.Max(s => s.Id) + 1 : 1;
                student.RegistrationDate = DateTime.Now;
                _students.Add(student);
            }

            // User notification message
            TempData["SuccessMessage"] = $"Registration successful! {student.Name} has been enrolled in {student.Course}.";

            // Redirect to Student List page (POST-Redirect-GET pattern)
            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // GET: /Student/Details/{id}  --  Student Details Page
        // ============================================================
        public IActionResult Details(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                TempData["ErrorMessage"] = $"Student with ID #{id} was not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }
    }
}
