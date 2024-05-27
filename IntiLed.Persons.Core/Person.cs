using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntiLed.Persons.Core
{
    public class Person
    {
        #region Private Fields
        private string name = string.Empty;
        private string surname = string.Empty;
        private int age = 16;
        private string city = string.Empty;
        #endregion

        #region Public Properties
        public string Name
        {
            get => name; 
            set => name = value;
        }
        public string Surname
        {
            get => surname;
            set => surname = value;
        }
        public int Age
        {
            get => age;
            set => age = value;
        }
        public string City
        {
            get => city; 
            set => city = value;
        }
        #endregion

        #region .ctor
        public Person()
        {
            
        }
        #endregion

        #region override
        public override string ToString()
        {
            return $"{Name}; {Surname}; {Age}; {City}";
        }
        #endregion
    }
}
