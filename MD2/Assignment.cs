
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //5.uzdevums
    public class Assignment
    {
        //īpašība dealine ar tipu date
        public DateOnly DeadLine { get; set; }
        //īpašība course ar tipu course
        public Course Course { get; set; }
        //īpāsība description ar tipu teksts
        public string Description { get; set; }
        public bool IsEditing { get; set; } = false;
        public DateTime DeadLineDateTime => DeadLine.ToDateTime(TimeOnly.MinValue);


        //Pārdefinēta Tostring metode
        public override string ToString()
        {
            return $"Deadline: {DeadLine}, Course: {Course}, Description: {Description}";
        }

    }
}