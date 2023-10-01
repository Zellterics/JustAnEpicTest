using System;
using System.Collections.Generic;
using System.Threading;

namespace JustAnEpicTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map("../../Map.csv");
            String onTop = "1";
            Character player = new Character("C", new Character.Pair(1, 1), "../../character.json");
            int deltaTime = 40;
            Character.Pair lastPosition;
            player.LoadData();
            Console.WriteLine("LOADING...");
            Thread.Sleep(3000);
            Portal portal = new Portal(new Character.Pair(5, 1), new Character.Pair(30, 30));
            map.SetStringOn(portal.p1.x, portal.p1.y, "P");
            map.SetStringOn(portal.p2.x, portal.p2.y, "P");
            do
            {
                //Update Player
                player.SaveData(player);
                map.SetStringOn(player.position.x, player.position.y, onTop);
                lastPosition = player.position;
                player.Move(HandleInput());
                if(map.GetStringOn(player.position.x, player.position.y) == "P")
                {
                    if(onTop != "P")
                    {
                        player.position = portal.OnPortalEntered(player.position);
                    }
                    onTop = "P";
                }
                if(map.GetStringOn(player.position.x, player.position.y) == "0")
                {
                    player.position = lastPosition;
                }
                if(map.GetStringOn(player.position.x, player.position.y) == "1")
                {
                    onTop = "1";
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
