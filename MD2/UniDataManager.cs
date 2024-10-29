using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //9.uzdevums
    // UniDataManager klase, kas implementē IDataManager interfeisu
    public class UniDataManager : IDataManager
    {
        public List<Teacher> Teachers { get; private set; } = new List<Teacher>();
        public List<Student> Students { get; private set; } = new List<Student>();
        public List<Course> Courses { get; private set; } = new List<Course>();
        public List<Assignment> Assignments { get; private set; } = new List<Assignment>();
        public List<Submission> Submissions { get; private set; } = new List<Submission>();


        private Uni uniData;
        public UniDataManager()
        {
            uniData = new Uni();

        }


    


    //Testa dati prieks 2-6 punkta klasēm
    public void CreateTestData()
        {
            Teachers = new List<Teacher>{ };
            var teacher1 = new Teacher("Zane", "Biete", Person.Gender.Woman, new DateTime(2024, 6, 10));
            var teacher2 = new Teacher("Juris", "Zilais", Person.Gender.Man, new DateTime(2024, 5, 10));

            uniData.AddTeacher(teacher1);
            uniData.AddTeacher(teacher2);

            Teachers.AddRange(uniData.Teachers);

            Students = new List<Student> { };

            var student1 = new Student("Signe", "Siliņa", Person.Gender.Woman, "SS12345");
            var student2 = new Student("Pēteris", "Kartupelis", Person.Gender.Man, "KP54321");

            uniData.AddStudent(student1);
            uniData.AddStudent(student2);
            Students.AddRange(uniData.Students);

            Courses = new List<Course> { };
            var course1 = new Course { Name = ".NET", Teacher = teacher1 };
            var course2 = new Course { Name = "JavaScript", Teacher = teacher2 };

            uniData.AddCourse(course1);
            uniData.AddCourse(course2);
            Courses.AddRange(uniData.Courses);

            Assignments = new List<Assignment> { };
            var assignment1 = new Assignment { DeadLine = new DateOnly(2024, 10, 31), Course = course1, Description = "MD1" };
            var assignment2 = new Assignment { DeadLine = new DateOnly(2024, 11, 15), Course = course2, Description = "MD1" };

            uniData.AddAssignment(assignment1);
            uniData.AddAssignment(assignment2);
            Assignments.AddRange(uniData.Assignments);

            Submissions = new List<Submission> { };
            var submission1 = new Submission { Assignement = assignment1, Student = student1, SubmissionTime = DateTime.Now, Score = 95 };
            var submission2 = new Submission { Assignement = assignment2, Student = student2, SubmissionTime = DateTime.Now, Score = 88 };

            uniData.AddSubmission(submission1);
            uniData.AddSubmission(submission2);
            Submissions.AddRange(uniData.Submissions);
        }
        //Print metode
        public string Print()
        {
            var output = new StringWriter();

            output.WriteLine("Teachers:");
            foreach (var teacher in uniData.Teachers)
            {
                output.WriteLine(teacher.ToString());
            }

            output.WriteLine("\nStudents:");
            foreach (var student in uniData.Students)
            {
                output.WriteLine(student.ToString());
            }

            output.WriteLine("\nCourses:");
            foreach (var course in uniData.Courses)
            {
                output.WriteLine(course.ToString());
            }

            output.WriteLine("\nAssignments:");
            foreach (var assignment in uniData.Assignments)
            {
                output.WriteLine(assignment.ToString());
            }

            output.WriteLine("\nSubmissions:");
            foreach (var submission in uniData.Submissions)
            {
                output.WriteLine($"Assignment: {submission.Assignement.Description}, Student: {submission.Student.FullName}, Submission Time: {submission.SubmissionTime}, Score: {submission.Score}");
            }

            return output.ToString();
        }

        //Save metode saglabā datus uz JSON failu
        public void Save(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonData = JsonSerializer.Serialize(uniData, options);
            File.WriteAllText(filePath, jsonData);
        }
        //Load metode, kas nolasa datus no JSON faila
        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            var jsonData = File.ReadAllText(filePath);
            uniData = JsonSerializer.Deserialize<Uni>(jsonData);

            // Update the public lists based on uniData
            Teachers = uniData.Teachers;
            Students = uniData.Students;
            Courses = uniData.Courses;
            Assignments = uniData.Assignments;
            Submissions = uniData.Submissions;
        }

        //Reset metode, izdzēš datus
        public void Reset()
        {
            uniData = new Uni();
        }
    }

}
