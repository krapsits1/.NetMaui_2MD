using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //3.Uzdevums
    //Klases Person apakšklase Student
    public class Student : Person
    {
        //Īpašība studnetidNumber
        public string StudentIdNumber { get; set; }

        //Konstruktors 1
        public Student(string name, string surname, Gender gender, string StudentIdNumber)
        {
            this.Name = name;
            this.Surname = surname;
            this.GenderType = gender;
            this.StudentIdNumber = StudentIdNumber;
        }

        //Konstruktors 2
        public Student() { }
        //Pārdefinēta Tostring metode
        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, Fullname: {FullName}, Gender: {GenderType}, Student ID: {StudentIdNumber}";
        }

    }
}
