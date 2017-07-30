using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class Department
    {
        private string department;

        public string _Department
        {
            get { return department; }
            set
            {
                department = value;
            }
        }
        

        public static Department Clone(Department oldDepartment)
        {
            Department newDepartment = new Department();
            newDepartment.department = oldDepartment.department;
            return newDepartment;
        }
    }
}
 