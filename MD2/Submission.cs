using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //6.Uzdevums
    public class Submission
    {
        //īpašība assignment, tips assignement
        public Assignment Assignement { get; set; }
        //īpašība students ar tipu students
        public Student Student { get; set; }
        //īpašība SubmissionTime ar tipu datums nu laiks
        public DateTime SubmissionTime { get; set; }
        //īpašība Score ar tipu vesels skaitlis
        public int Score { get; set; }

        public bool IsEditing { get; set; } = false;

    }
}
