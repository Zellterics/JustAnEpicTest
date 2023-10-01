using System;

namespace JustAnEpicTest
{
    public struct Pair
    {
        public int x { get; set; }
        public int y { get; set; }

        public Pair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Pair))
            {
                return false;
            }

            Pair other = (Pair)obj;

            return this.x == other.x && this.y == other.y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }
        public static bool operator ==(Pair p1, Pair p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }
        public static bool operator !=(Pair p1, Pair p2)
        {
            return !(p1.x == p2.x && p1.y == p2.y);
        }
    }
    public enum Direction
    {
        up, down, left, right, none
    }
}
