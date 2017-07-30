using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    /// <summary>
    /// Stores all data related to a Location that is needed in computation
    /// </summary>
    public class Location
    {
        /// <summary>
        /// The string location of this location
        /// </summary>
        private string location;
        public string _Location
        {
            get { return location; }
            set
            {
                location = value;
            }
        }

        /// <summary>
        /// makes a deep clone of the Location passed in. Used primarily in persisting to database.
        /// </summary>
        /// <param name="oldPrize">the Location to clone</param>
        /// <returns>the cloned Location</returns>
        public static Location Clone(Location oldLocation)
        {
            Location newLocation = new Location();
            newLocation.location = oldLocation.location;
            return newLocation;
        }
    }
}
