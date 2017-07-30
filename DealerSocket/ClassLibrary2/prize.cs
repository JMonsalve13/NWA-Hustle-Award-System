using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class Prize
    {
        private string prizeName;
        private decimal val;
        private bool isActive;
        private string picPath;
        private string desc;

        public string PrizeName
        {
            get { return prizeName; }
            set
            {
                prizeName = value;
            }
        }

        public decimal Value
        {
            get { return val; }
            set
            {
                val = value;
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
            }
        }

        public string PicPath
        {
            get { return picPath; }
            set
            {
                picPath = value;
            }
        }

        public string Desc
        {
            get { return desc; }
            set
            {
                desc = value;
            }
        }


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

