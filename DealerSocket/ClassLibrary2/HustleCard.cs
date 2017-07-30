using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class HustleCard
    {
        private int personID;
        private int cardID;
        private string personReceiving;
        private string personGiving;
        private string personReceivingLocation;
        private string personReceivingDepartment;
        private string date;
        private string reasonForCard;

        public int PersonID
        {
            get { return personID; }
            set { personID = value; }
        }
        public int CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }

        public string PersonReceiving
        {
            get { return personReceiving; }
            set { personReceiving = value; }
        }
        public string PersonGiving
        {
            get { return personGiving; }
            set { personGiving = value; }
        }
        public string PersonReceivingLocation
        {
            get { return personReceivingLocation; }
            set { personReceivingLocation = value; }
        }
        public string PersonReceivingDepartment
        {
            get { return personReceivingDepartment; }
            set { personReceivingDepartment = value; }
        }

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
            }
        }

        public string ReasonForCard
        {
            get { return reasonForCard; }
            set { reasonForCard = value; }
        }

        public static HustleCard Clone(HustleCard oldHustleCard)
        {
            HustleCard newHustleCard = new HustleCard();

            newHustleCard.cardID = oldHustleCard.cardID;
            newHustleCard.personID = oldHustleCard.personID;
            newHustleCard.personReceiving = oldHustleCard.personReceiving;
            newHustleCard.personGiving = oldHustleCard.personGiving;
            newHustleCard.PersonReceivingDepartment = oldHustleCard.PersonReceivingDepartment;
            newHustleCard.personReceivingLocation = oldHustleCard.personReceivingLocation;
            newHustleCard.reasonForCard = oldHustleCard.reasonForCard;
            newHustleCard.date = oldHustleCard.date;

            return newHustleCard;
        }

    }
}
