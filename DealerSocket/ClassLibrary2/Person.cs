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
                firstName = FirstName;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = LastName;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = Email;
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                department = Department;
            }

        }

        public string Location
        {
            get { return location; }
            set
            {
                location = Location;
            }
        }

        public int ID
        {
            get { return id; }
            set
            {
                id = ID;
            }
        }

        public static Person ClonePerson(Person OldPerson)
        {
            Person newPerson = new Person();

            newPerson.firstName = OldPerson.firstName;
            newPerson.lastName = OldPerson.lastName;
            newPerson.location = OldPerson.location;
            newPerson.id = OldPerson.id;
            newPerson.department = OldPerson.department;
            newPerson.email = newPerson.email;

            return newPerson;
        }
    }
}
