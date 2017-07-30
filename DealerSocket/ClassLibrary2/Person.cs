using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class Person
    {
        private string firstName;
        private string lastName;
        private string email;
        private string department;
        private string location;
        private int id;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                department = value;
            }

        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
            }
        }

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public static Person Clone(Person OldPerson)
        {
            Person newPerson = new Person();

            newPerson.firstName = OldPerson.firstName;
            newPerson.lastName = OldPerson.lastName;
            newPerson.location = OldPerson.location;
            newPerson.id = OldPerson.id;
            newPerson.department = OldPerson.department;
            newPerson.email = OldPerson.email;

            return newPerson;
        }
    }
}
