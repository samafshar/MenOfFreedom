using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public	class GunItemInfo
	{
        private GunItem.GunNameEnum name;

        public GunItem.GunNameEnum Name
        {
            get { return name; }
            set { name = value; }
        }

       
        private int bulletCount;

        public int BulletCount
        {
            get { return bulletCount; }
            set { bulletCount = value; }
        }
	}

