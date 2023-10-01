using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAnEpicTest
{
    internal class Portal
    {
        public Character.Pair p1;
        public Character.Pair p2;
        public Portal(Character.Pair position1, Character.Pair position2) {
            p1 = position1;
            p2 = position2;
        }
        public Character.Pair OnPortalEntered(Character.Pair position)
        {
            if(position == p1)
            {
                return p2;
            }
            return p1;
        }
    }
}
