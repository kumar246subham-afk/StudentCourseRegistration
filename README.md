# 🎓 Marwadi University — Department of Computer Engineering
## .NET Technologies (01CE1523) &bull; Self-Learning Activity: ASP.NET Core MVC
### Practical Application: Student Course Registration System

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0_MVC-blue?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-purple?style=for-the-badge&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Marwadi University](https://img.shields.io/badge/Marwadi_University-NAAC_A%2B-red?style=for-the-badge)](https://www.marwadiuniversity.ac.in/)
[![Status](https://img.shields.io/badge/Build-Passing_(0_Errors)-success?style=for-the-badge)](https://github.com/kumar246subham-afk/StudentCourseRegistration)

---

## 📑 Report Structure (As per Assignment Guidelines)

### 1. Title Page & Academic Meta
- **Institution**: Marwadi University, Rajkot, Gujarat
- **Faculty / Department**: Department of Computer Engineering
- **Subject**: .NET Technologies (`01CE1523`)
- **Activity Title**: Self-Learning Activity &ndash; ASP.NET Core MVC
- **Program**: B.Tech Computer Engineering (Semester 5)
- **Student Name**: Subham Kumar Keshari
- **GitHub Repository**: [kumar246subham-afk/StudentCourseRegistration](https://github.com/kumar246subham-afk/StudentCourseRegistration)

---

### 2. Introduction
**ASP.NET Core MVC** is Microsoft's modern, cross-platform, open-source framework designed for building scalable web applications adhering to the **Model-View-Controller (MVC)** architectural design pattern. Unlike legacy .NET Framework ASP.NET, ASP.NET Core is lightweight, modular, container-ready, and runs seamlessly on Windows, Linux, and macOS.

This self-learning project applies ASP.NET Core MVC concepts by implementing an end-to-end **Student Course Registration System**.

---

### 3. Learning Objectives
1. Understand the core fundamentals of ASP.NET Core MVC and the request-response lifecycle.
2. Master the project structure (`Program.cs`, `appsettings.json`, `wwwroot`, `Models`, `Views`, `Controllers`).
3. Differentiate roles between Models (Data & Rules), Views (Razor UI), and Controllers (Request dispatching).
4. Implement Razor Views with Tag Helpers (`asp-for`, `asp-validation-for`, `asp-action`, `asp-controller`).
5. Execute Form handling, Model Binding, and robust server-side validation using **Data Annotations**.
6. Verify and enforce business logic using `ModelState.IsValid`.
7. Implement the **POST-Redirect-GET (PRG)** pattern to prevent duplicate submissions.

---

### 4. ASP.NET Core MVC Concepts & MVC Architecture

```
                                      [HTTP Request]
                                            │
                                            ▼
                                   ┌─────────────────┐
                                   │     Routing     │
                                   │   (Program.cs)  │
                                   └────────┬────────┘
                                            │
                                            ▼
                                   ┌─────────────────┐
                                   │   Controller    │ ◄─── Model Binding
                                   │(StudentController)    (HTTP Form Data)
                                   └────────┬────────┘
                                            │
                         ┌──────────────────┴──────────────────┐
                         │                                     │
           [Valid: ModelState.IsValid]           [Invalid: Validation Errors]
                         │                                     │
                         ▼                                     ▼
                ┌─────────────────┐                   ┌─────────────────┐
                │   Save Model    │                   │   Return Form   │
                │(In-Memory Store)│                   │   with Errors   │
                └────────┬────────┘                   └────────┬────────┘
                         │                                     │
                         ▼                                     ▼
                ┌─────────────────┐                   ┌─────────────────┐
                │ RedirectToAction│                   │      View       │
                │     (Index)     │                   │  (Create.cshtml)│
                └────────┬────────┘                   └─────────────────┘
                         │
                         ▼
                ┌─────────────────┐
                │   Razor View    │
                │  (Index.cshtml) │
                └─────────────────┘
```

- **Model (`Student.cs`)**: Represents the entity, defines fields (`Id`, `Name`, `Email`, `Course`, `Semester`), and enforces validation rules via Data Annotations.
- **Controller (`StudentController.cs`)**: Coordinates workflow, handles HTTP GET/POST requests, tests `ModelState.IsValid`, updates storage, and directs navigation.
- **View (`Views/Student/*.cshtml`)**: Razor templates that render HTML, utilize Tag Helpers for clean markup, and display validation feedback.

---

### 5. Project Directory Structure

```
StudentCourseRegistration/
├── Controllers/
│   ├── HomeController.cs          # Landing page with activity flow chart
│   └── StudentController.cs       # Core CRUD actions: Index, Create (GET/POST), Details
├── Models/
│   ├── Student.cs                 # Student entity with Data Annotations
│   └── ErrorViewModel.cs          # Error handling model
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           # Home page explaining application flow
│   │   └── Privacy.cshtml
│   ├── Student/
│   │   ├── Index.cshtml           # Student List View
│   │   ├── Create.cshtml          # Registration Form View with validation
│   │   └── Details.cshtml         # Student Details Profile View
│   └── Shared/
│       ├── _Layout.cshtml         # University branded master layout
│       └── _ValidationScriptsPartial.cshtml # jQuery Unobtrusive validation
├── Properties/
│   └── launchSettings.json        # Port configurations (5000 / 5226)
├── wwwroot/                       # Static files (Bootstrap 5, jQuery, CSS, JS)
├── appsettings.json               # Application configuration
├── Program.cs                     # Startup configuration & default route
└── StudentCourseRegistration.csproj # Project file (.NET 8.0)
```

---

### 6. Application Description & Specification Table

| Component | Target Name | Implementation Details |
| :--- | :--- | :--- |
| **Model** | `Student.cs` | Fields: `Id`, `Name`, `Email`, `Course`, `Semester`, `RegistrationDate`<br />Decorated with `[Required]`, `[StringLength]`, `[Range]`, and `[EmailAddress]`. |
| **Controller** | `StudentController.cs` | Actions: `Index()`, `Create()` (GET & POST), `Details(int id)`<br />Uses in-memory `static List<Student>` data persistence. |
| **Views** | `Views/Student/` | `Index.cshtml`: List of all registered students with badges and links.<br />`Create.cshtml`: Form with Tag Helpers and validation spans.<br />`Details.cshtml`: Profile card showing complete student information. |
| **Routing** | `Program.cs` | Default Route: `{controller=Home}/{action=Index}/{id?}`. |

---

### 7. Important Code Snippets

#### A. Model Definition with Data Annotations (`Models/Student.cs`)
```csharp
public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Student Name is required.")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 60 characters.")]
    [Display(Name = "Student Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email Address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course selection is required.")]
    public string Course { get; set; } = string.Empty;

    [Required(ErrorMessage = "Semester is required.")]
    [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
    public int Semester { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}
```

#### B. Controller Action with `ModelState.IsValid` (`Controllers/StudentController.cs`)
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Student student)
{
    if (!ModelState.IsValid)
    {
        // Validation Failed: Re-render form with validation error messages
        ViewBag.Courses = AvailableCourses;
        return View(student);
    }

    // Validation Succeeded: Persist student record
    lock (_students)
    {
        student.Id = _students.Count > 0 ? _students.Max(s => s.Id) + 1 : 1;
        student.RegistrationDate = DateTime.Now;
        _students.Add(student);
    }

    TempData["SuccessMessage"] = $"Registration successful! {student.Name} has been enrolled.";
    return RedirectToAction(nameof(Index)); // POST-Redirect-GET Pattern
}
```

#### C. Tag Helpers in Razor View (`Views/Student/Create.cshtml`)
```html
<form asp-action="Create" method="post">
    <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>

    <div class="mb-3">
        <label asp-for="Name" class="form-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger small"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Email" class="form-label"></label>
        <input asp-for="Email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger small"></span>
    </div>
    ...
</form>
```

---

### 8. Application Flow & Screenshots Simulation

1. **Home Page (`/`)**: Displays university branding, introduction, learning outcomes, application flow diagram, and quick buttons to Register or View Students.
2. **Registration Form (`/Student/Create`)**: Form displaying fields for Student Name, Email Address, Course Dropdown, and Semester.
3. **Validation Errors**:
   - Submitting an empty form triggers required field alerts.
   - Entering invalid email (e.g. `abc`) triggers `"Please enter a valid email address."`
   - Entering semester outside 1–8 triggers `"Semester must be between 1 and 8."`
4. **Student List (`/Student/Index`)**: Only registrations passing `ModelState.IsValid` appear here in an organized table with badges and details buttons.
5. **Student Details (`/Student/Details/1`)**: Displays complete profile with registration timestamp and navigation buttons.

---

### 9. Challenges Faced & Solutions

| Challenge Encountered | Technical Solution Applied |
| :--- | :--- |
| **Form re-submission on page refresh** | Implemented the **POST-Redirect-GET (PRG)** pattern using `RedirectToAction("Index")` after successful POST. |
| **Missing client-side error spans** | Added `_ValidationScriptsPartial.cshtml` containing `jquery.validate.unobtrusive.js` to enable real-time inline validation before form submission. |
| **Thread safety for in-memory list** | Used `lock (_students)` during ID generation and list addition to prevent race conditions during concurrent user submissions. |
| **State preservation on validation failure** | Repopulated `ViewBag.Courses` when returning `View(student)` upon invalid model state so the dropdown remains intact. |

---

### 10. Learning Outcomes Achieved
- Mastered building an ASP.NET Core 8.0 MVC project from scratch.
- Implemented strong type-safety using C# Models and Data Annotations.
- Mastered Razor Tag Helpers (`asp-for`, `asp-action`, `asp-controller`, `asp-validation-for`).
- Gained hands-on experience enforcing validation with `ModelState.IsValid`.
- Gained confidence in GitHub version control and project documentation.

---

### 11. How to Run the Project Locally

```bash
# Clone the repository
git clone https://github.com/kumar246subham-afk/StudentCourseRegistration.git

# Navigate to project directory
cd StudentCourseRegistration

# Restore and run
dotnet restore
dotnet run
```
Open **`http://localhost:5000`** in any browser.

---

### 12. References & Video Resources (Section 10)
1. Microsoft Learn: [Overview of ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)
2. Microsoft Learn: [Model validation in ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation)
3. freeCodeCamp: *Learn ASP.NET Core MVC with .NET 9*
4. kudvenkat: *ASP.NET Core Tutorial for Beginners (YouTube Playlist)*
5. Marwadi University: *.NET Technologies (01CE1523) Curriculum Material*

---

**Developed with ❤️ by Subham Kumar Keshari**  
B.Tech Computer Engineering &bull; Marwadi University
