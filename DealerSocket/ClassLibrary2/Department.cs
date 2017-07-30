using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    /// <summary>
    /// Stores all data related to a Department that is needed in computation
    /// </summary>
    public class Department
    {
        /// <summary>
        /// The string representation of the department
        /// </summary>
        private string department;
        public string _Department
        {
            get { return department; }
            set
            {
                department = value;
            }
        }

        /// <summary>
        /// makes a deep clone of the Department passed in. Used primarily in persisting to database.
        /// </summary>
        /// <param name="oldPrize">the Department to clone</param>
        /// <returns>the cloned Department</returns>
        public static Department Clone(Department oldDepartment)
        {
            Department newDepartment = new Department();
            newDepartment.department = oldDepartment.department;
            return newDepartment;
        }
    }
}
 