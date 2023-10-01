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
            Pair other = new Pair(1, 1);

            return Object.ReferenceEquals(null, other) ? false : x == other.x;
        }
        public override int GetHashCode()
        {
            return x;
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
