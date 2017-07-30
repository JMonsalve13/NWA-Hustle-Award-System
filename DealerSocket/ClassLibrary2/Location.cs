using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class Location
    {
        private string location;
        public string _Location
        {
            get { return location; }
            set
            {
                location = value;
            }
        }

        public static Location Clone(Location oldLocation)
        {
            Location newLocation = new Location();
            newLocation.location = oldLocation.location;
            return newLocation;
        }


    }
}
