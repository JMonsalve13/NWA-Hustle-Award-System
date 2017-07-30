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
            set { personID = PersonID; }
        }
        public int CardID
        {
            get { return cardID; }
            set { cardID = CardID; }
        }

        public string PersonReceiving
        {
            get { return personReceiving; }
            set { personReceiving = PersonReceiving; }
        }
        public string PersonGiving
        {
            get { return personGiving; }
            set { personGiving = PersonGiving; }
        }
        public string PersonReceivingLocation
        {
            get { return personReceivingLocation; }
            set { personReceivingLocation = PersonReceivingLocation; }
        }
        public string PersonReceivingDepartment
        {
            get { return personReceivingDepartment; }
            set { personReceivingDepartment = PersonReceivingDepartment; }
        }

        public string Date
        {
            get { return date; }
            set
            {
                date = Date;
            }
        }

        public string ReasonForCard
        {
            get { return reasonForCard; }
            set { reasonForCard = ReasonForCard; }
        }

        public static HustleCard cloneHustleCard(HustleCard oldHustleCard)
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
        }

    }
}
