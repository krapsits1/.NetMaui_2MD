using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //2.Uzdevums
    //Person apakšklase Teacher
    public class Teacher : Person
    {
        //Īpašība Constract date, kas ir datums
        public DateTime ContractDate { get; set; }
        //Konstruktors, lai Teachaer instancei varētu ērti pievieonot vērtības
        public Teacher(string name, string surname, Gender gender, DateTime contractDate)
        {
            this.Name = name;
            this.Surname = surname;
            this.GenderType = gender;
            this.ContractDate = contractDate;
        }
        //Konstruktors
        public Teacher() { }

        //Pārdefinēta bāzes klase "ToString", lai atgriež īpašību teskstu kā tekstu
        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, Fullname: {FullName}, Gender: {GenderType}, Contract Date: {ContractDate.ToShortDateString()}";
        }

    }
}
