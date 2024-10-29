using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //4.Uzdevums
    public class Course
    {
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        //Pārdefinēta Tostring metode
        public override string ToString()
        {
            return $"Course Name: {Name}, Teacher: {Teacher.ToString()}";
        }
    }

}
