using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Projekts.Models
{
    //1. uzdevums
    //Izveidot abstraktu klasi „Person” 
    public abstract class Person
    {   //Privāti atribūti
        private string name;
        private string surname;

        //Īpašības Name, Surname, Fullname, Gender
        public string Name
        {
            get { return name; }
            set
            {   //Ja Name vērtība ir tukša, tad piešķiram iepriekšējo
                if (value != null)
                {
                    name = value;
                }
            }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        //FullName īpašība tikai lasīšanai, jo nav "set" vērtības.
        //Vārda un uzvārda konketanācija ar atstarpi starp vērtībām.
        public string FullName
        {
            get { return name + " " + surname; }
        }
        //Gender īpašiba ar pārskaitāmu tipu "enum". Divas vērtības Man un Woman
        public enum Gender
        {
            Man, Woman
        }

        private Gender gender;
        public Gender GenderType
        {
            get { return gender; }
            set { gender = value; }
        }
        //Pārdefinēta bāzes klase "ToString", lai atgriež īpašību teskstu kā tekstu
        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, Fullname: {FullName}, Gender: {GenderType}";
        }
    }
}
