using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class Location
    {
        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = location;
            }
        }

        public static Location CloneLocation(Location oldLocation)
        {
            Location newLocation = new Location();
            newLocation.location = oldLocation.location;
        }


    }
}
