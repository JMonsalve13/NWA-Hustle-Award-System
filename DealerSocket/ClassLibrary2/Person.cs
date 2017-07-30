using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    /// <summary>
    /// Stores all data related to a Person that is needed in computation
    /// </summary>
    public class Person
    {
        /// <summary>
        /// This Person's first name
        /// </summary>
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }

        /// <summary>
        /// This Person's last name
        /// </summary>
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
            }
        }

        /// <summary>
        /// This person's email
        /// </summary>
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// The department in which this person works
        /// </summary>
        private string department;
        public string Department
        {
            get { return department; }
            set
            {
                department = value;
            }

        }

        /// <summary>
        /// The location where this person works
        /// </summary>
        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
            }
        }
        
        /// <summary>
        /// makes a deep clone of the Person passed in. Used primarily in persisting to database.
        /// </summary>
        /// <param name="oldPrize">the Person to clone</param>
        /// <returns>the cloned Person</returns>
        public static Person Clone(Person OldPerson)
        {
            Person newPerson = new Person();

            newPerson.firstName = OldPerson.firstName;
            newPerson.lastName = OldPerson.lastName;
            newPerson.location = OldPerson.location;
            newPerson.department = OldPerson.department;
            newPerson.email = OldPerson.email;

            return newPerson;
        }
    }
}
