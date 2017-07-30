using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    /// <summary>
    /// Stores all data related to a HustleCard that is needed in computation
    /// </summary>
    public class HustleCard
    {
        /// <summary>
        /// The person for whom the HustleCard was filed
        /// </summary>
        private string personReceiving;
        public string PersonReceiving
        {
            get { return personReceiving; }
            set { personReceiving = value; }
        }

        /// <summary>
        /// The person who filed the HustleCard
        /// </summary>
        private string personGiving;
        public string PersonGiving
        {
            get { return personGiving; }
            set { personGiving = value; }
        }
        
        /// <summary>
        /// The date when this card was awarded
        /// </summary>
        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
            }
        }

        /// <summary>
        /// The commented reason for awarding this HustleCard
        /// </summary>
        private string reasonForCard;
        public string ReasonForCard
        {
            get { return reasonForCard; }
            set { reasonForCard = value; }
        }

        /// <summary>
        /// makes a deep clone of the HustleCard passed in. Used primarily in persisting to database.
        /// </summary>
        /// <param name="oldPrize">the HustleCard to clone</param>
        /// <returns>the cloned HustleCard</returns>
        public static HustleCard Clone(HustleCard oldHustleCard)
        {
            HustleCard newHustleCard = new HustleCard();
            
            newHustleCard.personReceiving = oldHustleCard.personReceiving;
            newHustleCard.personGiving = oldHustleCard.personGiving;
            newHustleCard.reasonForCard = oldHustleCard.reasonForCard;
            newHustleCard.date = oldHustleCard.date;

            return newHustleCard;
        }
    }
}
