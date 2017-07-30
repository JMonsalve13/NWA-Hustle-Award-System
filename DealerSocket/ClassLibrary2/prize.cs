using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    public class Prize
    {
        private string prizeName;
        private decimal value;
        private bool isActive;
        private string picPath;
        private string desc;

        public string PrizeName
        {
            get { return prizeName; }
            set
            {
                prizeName = prizeName;
            }
        }

        public decimal Value
        {
            get { return value; }
            set
            {
                value = Value;
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = isActive;
            }
        }

        public string PicPath
        {
            get { return picPath; }
            set
            {
                picPath = PicPath;
            }
        }

        public string Desc
        {
            get { return desc; }
            set
            {
                desc = Desc;
            }
        }


        public static Prize ClonePrize(Prize oldPrize)
        {
            Prize newPrize = new Prize();
            newPrize.prizeName = oldPrize.prizeName;
            newPrize.value = oldPrize.value;
            newPrize.picPath = oldPrize.picPath;
            newPrize.isActive = oldPrize.isActive;
            newPrize.desc = oldPrize.desc;

            return newPrize;
        }





    }
}

