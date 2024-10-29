using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //7. Izveidot klasi (vai klases), kas satur kolekcijas (vai kādas citas datu struktūras) ar punktos 1-6 minēto klašu instancēm.
    public class Uni
    {
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Submission> Submissions { get; set; } = new List<Submission>();

        // Metode, lai pievienotu jaunu skolotāju sarakstam
        public void AddTeacher(Teacher teacher)
        {
            Teachers.Add(teacher);
        }

        // Metode, lai pievienotu jaunu studentu sarakstam
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        // Metode, lai pievienotu jaunu kursu sarakstam
        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        // Metode, lai pievienotu jaunu uzdevumu sarakstam
        public void AddAssignment(Assignment assignment)
        {
            Assignments.Add(assignment);
        }
        //Metode, lia pievieonotu jaunu termiņu
        public void AddSubmission(Submission submission)
        {
            Submissions.Add(submission);
        }

    }

}
