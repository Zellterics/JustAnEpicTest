using System;
using System.Collections.Generic;
using System.Threading;

namespace JustAnEpicTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character.Pair startPosition = new Character.Pair(1, 1);
            Map map = new Map("../../Map.csv");
            String onTop = map.GetStringOn(startPosition.x, startPosition.y);
            Character player = new Character("C", startPosition, "../../character.json");
            int deltaTime = 40;
            Character.Pair lastPosition;
            player.LoadData();
            do
            {
                //Update Player
                player.SaveData(player);
                map.SetStringOn(player.position.x, player.position.y, onTop);
                onTop = map.GetStringOn(player.position.x, player.position.y);
                lastPosition = player.position;
                player.Move(HandleInput());
                if(map.GetStringOn(player.position.x, player.position.y) != "1")
                {
                    player.position = lastPosition;
                }
                map.SetStringOn(player.position.x, player.position.y, player.symbol);

                //Update Map
                Thread.Sleep(deltaTime);
                Console.Clear();
                map.Print();
            } while (true);
        }

        static Character.Direction HandleInput()
        {
            ConsoleKeyInfo input;
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);
            }
            else
            {
                return Character.Direction.none;
            }
            Dictionary<ConsoleKey, Character.Direction> keyToDirection = new Dictionary<ConsoleKey, Character.Direction>
            {
                { ConsoleKey.W, Character.Direction.up },
                { ConsoleKey.A, Character.Direction.left },
                { ConsoleKey.S, Character.Direction.down },
                { ConsoleKey.D, Character.Direction.right },
                { ConsoleKey.UpArrow, Character.Direction.up },
                { ConsoleKey.LeftArrow, Character.Direction.left },
                { ConsoleKey.DownArrow, Character.Direction.down },
                { ConsoleKey.RightArrow, Character.Direction.right }
            };
            if (keyToDirection.TryGetValue(input.Key, out Character.Direction direction))
            {
                return direction;
            }
            else
            {
                return Character.Direction.none;
            }
        }
    }
}
