using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    /// <summary>
    /// Stores all data related to a Prize that is needed in computation
    /// </summary>
    public class Prize
    {
        /// <summary>
        /// The mane of the prize
        /// </summary>
        private string prizeName;
        public string PrizeName
        {
            get { return prizeName; }
            set
            {
                prizeName = value;
            }
        }
        /// <summary>
        /// The monetary value of the prize
        /// </summary>
        private string val;
        public string Value
        {
            get { return val; }
            set
            {
                val = value;
            }
        }
        /// <summary>
        /// Determines if the prize is still active for drawing or not.
        /// </summary>
        private string isActive;
        public string IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
            }
        }
        /// <summary>
        /// The link or path to this prize's image
        /// </summary>
        private string picPath;
        public string PicPath
        {
            get { return picPath; }
            set
            {
                picPath = value;
            }
        }
        /// <summary>
        /// Access the decription of the prize
        /// </summary>
        private string desc;
        public string Desc
        {
            get { return desc; }
            set
            {
                desc = value;
            }
        }

        /// <summary>
        /// makes a deep clone of the prize passed in. Used primarily in persisting to database.
        /// </summary>
        /// <param name="oldPrize">the prize to clone</param>
        /// <returns>the cloned prize</returns>
        public static Prize Clone(Prize oldPrize)
        {
            Prize newPrize = new Prize();

            newPrize.prizeName = oldPrize.prizeName;
            newPrize.val = oldPrize.val;
            newPrize.picPath = oldPrize.picPath;
            newPrize.isActive = oldPrize.isActive;
            newPrize.desc = oldPrize.desc;

            return newPrize;
        }
    }
}

