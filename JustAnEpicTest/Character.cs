using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAnEpicTest
{
    internal class Character
    {
        public struct Pair
        {
            public int x;
            public int y;
            public Pair(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        public enum Direction
        {
            up, down, left, right, none
        }
        public Pair position;
        public String symbol;

        public Character(String symbol, Pair position) {
            this.symbol = symbol;
            this.position = position;
        }
        public bool Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.up:
                    position.y--;
                    break;
                case Direction.down:
                    position.y++;
                    break;
                case Direction.left:
                    position.x--;
                    break;
                case Direction.right:
                    position.x++;
                    break;
                case Direction.none:
                    break;
            }
            return false;
        }
    }
}
