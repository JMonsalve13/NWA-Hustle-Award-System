using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class Department
    {
        private string department;

        public string Department
        {
            get { return department; }
            set
            {
                department = Department;
            }
        }
        

        public static Department CloneDepartment(Department oldDepartment)
        {
            Department newDepartment = new Department();
            newDepartment.department = oldDepartment.department;
            return newDepartment;
        }
    }
}
 