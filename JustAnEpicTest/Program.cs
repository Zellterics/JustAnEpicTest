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
            Character player = new Character("C", new Pair(1, 1), "../../character.json");
            int deltaTime = 40;
            Portal portal = new Portal(new Pair(5, 1), new Pair(30, 30));
            Initialize(ref player, ref map, portal);
            do
            {
                player.SaveData(player);
                map.SetStringOn(player.position, onTop);
                PlayerMove(ref player, map, ref onTop, portal);
                map.SetStringOn(player.position, player.symbol);
                UpdateMap(map, deltaTime);
            } while (true);
        }

        static void PlayerMove(ref Character player, Map map, ref String onTop, Portal portal)
        {
            Pair lastPosition = player.position;
            player.Move(HandleInput());
            String currentTile = map.GetStringOn(player.position);


            if (currentTile == "P")
            {
                if (onTop != "P")
                {
                    player.position = portal.OnPortalEntered(player.position);
                }
                onTop = "P";
            }
            if (currentTile == "0")
            {
                player.position = lastPosition;
            }
            if (currentTile == "1")
            {
                onTop = "1";
            }
        }

        static void UpdateMap(Map map, int deltaTime)
        {
            Thread.Sleep(deltaTime);
            Console.Clear();
            map.Print();
        }

        static void Initialize(ref Character player, ref Map map, Portal portal)
        {
            player.LoadData();
            Console.WriteLine("LOADING...");
            Thread.Sleep(3000);

            map.SetStringOn(portal.p1, "P");
            map.SetStringOn(portal.p2, "P");
        }

        static Direction HandleInput()
        {
            ConsoleKeyInfo input;
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);
            }
            else
            {
                return Direction.none;
            }
            Dictionary<ConsoleKey, Direction> keyToDirection = new Dictionary<ConsoleKey, Direction>
            {
                { ConsoleKey.W, Direction.up },
                { ConsoleKey.A, Direction.left },
                { ConsoleKey.S, Direction.down },
                { ConsoleKey.D, Direction.right },
                { ConsoleKey.UpArrow, Direction.up },
                { ConsoleKey.LeftArrow, Direction.left },
                { ConsoleKey.DownArrow, Direction.down },
                { ConsoleKey.RightArrow, Direction.right }
            };
            if (keyToDirection.TryGetValue(input.Key, out Direction direction))
            {
                return direction;
            }
            else
            {
                return Direction.none;
            }
        }
    }
}
