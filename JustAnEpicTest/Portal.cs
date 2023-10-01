using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAnEpicTest
{
    internal class Portal
    {
        public Pair p1;
        public Pair p2;
        public Portal(Pair position1, Pair position2) {
            p1 = position1;
            p2 = position2;
        }
        public Pair OnPortalEntered(Pair position)
        {
            if(position == p1)
            {
                return p2;
            }
            return p1;
        }
    }
}
