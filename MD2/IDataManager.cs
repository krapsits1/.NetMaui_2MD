using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    // Interfeiss IDataManager
    public interface IDataManager
    {
        List<Teacher> Teachers { get; }
        List<Student> Students { get; }
        List<Course> Courses { get; }
        List<Assignment> Assignments { get; }
        List<Submission> Submissions { get; }

        string Print();
        void Save(string filePath);
        void Load(string filePath);
        void CreateTestData();
        void Reset();

        public void CreateStudent(string name, string surname, Person.Gender gender, string idNumber)
        {
            var student = new Student
            {
                Name = name,
                Surname = surname,
                GenderType = gender,
                StudentIdNumber = idNumber
            };
            Students.Add(student);
        }

        public void CreateAssignment(string description, DateOnly deadline, Course course)
        {
            var assignment = new Assignment
            {
                Description = description,
                DeadLine = deadline,
                Course = course
            };
            Assignments.Add(assignment);
        }

        public void CreateSubmission(Assignment assignment, Student student, DateTime submissionTime, int score)
        {
            var submission = new Submission
            {
                Assignement = assignment,
                Student = student,
                SubmissionTime = submissionTime,
                Score = score
            };
            Submissions.Add(submission);
        }


    }

}
