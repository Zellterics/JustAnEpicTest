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
            int deltaTime = 30;

            //Create Portals
            List<Portal> portals = new List<Portal>();
            portals.Add(new Portal(new Pair(5, 1), new Pair(30, 30)));
            portals.Add(new Portal(new Pair(21, 19), new Pair(5, 28)));
            
            Initialize(ref player, ref map, portals);
            do
            {
                player.SaveData(player);
                map.SetStringOn(player.position, onTop);
                PlayerMove(ref player, map, ref onTop, portals);
                map.SetStringOn(player.position, player.symbol);
                UpdateMap(map, deltaTime);
            } while (player.state != 1);
            map.SaveMap();
        }

        static void PlayerMove(ref Character player, Map map, ref String onTop, List<Portal> portals)
        {
            Pair lastPosition = player.position;
            player.Move(HandleInput());
            String currentTile = map.GetStringOn(player.position);

            //COLLISION SYSTEM
            if (currentTile == "P" || currentTile == "p")
            {
                if (onTop != "P")
                {
                    foreach (Portal portal in portals)
                    {
                        if(portal.p1 == player.position || portal.p2 == player.position)
                        {
                            player.position = portal.OnPortalEntered(player.position);
                        }
                    }
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
            if(currentTile == "2")
            {
                onTop = "1";
            }


            //Illumation System
            map.CleanCharacters("2", "1");
            map.CleanCharacters("p", "P");
            map.CleanCharacters("3", "1");
            for (int i = 0; i < 4; i++)
            {
                Pair light = player.position;
                String lightChar = map.GetStringOn(light);
                for (int k = 0; lightChar != "0" && k < 6; k++)
                {
                    if (lightChar != "P" && lightChar != "p")
                    {
                        map.SetStringOn(light, "2");
                        for (int j = -1; j < 2; j++)
                        {
                            if (map.GetStringOn(new Pair(light.x + j, light.y)) == "1")
                            {
                                map.SetStringOn(new Pair(light.x + j, light.y), "3");
                            }
                            if (map.GetStringOn(new Pair(light.x + j, light.y)) == "P")
                            {
                                map.SetStringOn(new Pair(light.x + j, light.y), "p");
                            }
                        }
                        for (int j = -1; j < 2; j++)
                        {
                            if (map.GetStringOn(new Pair(light.x, light.y + j)) == "1")
                            {
                                map.SetStringOn(new Pair(light.x, light.y + j), "3");
                            }
                            if (map.GetStringOn(new Pair(light.x, light.y + j)) == "P")
                            {
                                map.SetStringOn(new Pair(light.x, light.y + j), "p");
                            }
                        }
                    }
                    else
                    {
                        map.SetStringOn(light, "p");
                        foreach (Portal portal in portals)
                        {
                            if (portal.p1 == light || portal.p2 == light)
                            {
                                map.SetStringOn(portal.OnPortalEntered(light), "p");
                            }
                        }
                    }

                    switch (i)
                    {
                        case 0:
                            light.y--;
                            break;
                        case 1:
                            light.y++;
                            break;
                        case 2:
                            light.x--;
                            break;
                        case 3:
                            light.x++;
                            break;
                    }
                    lightChar = map.GetStringOn(light);
                }

            }
        }


        static void UpdateMap(Map map, int deltaTime)
        {
            Thread.Sleep(deltaTime);
            Console.Clear();
            map.Print();
        }

        static void Initialize(ref Character player, ref Map map, List<Portal> portals)
        {
            player.LoadData();
            Console.WriteLine("LOADING...");
            Thread.Sleep(3000);

            foreach (Portal portal in portals)
            {
                map.SetStringOn(portal.p1, "P");
                map.SetStringOn(portal.p2, "P");
            }
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
                { ConsoleKey.RightArrow, Direction.right },
                {ConsoleKey.Escape, Direction.esc },
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
