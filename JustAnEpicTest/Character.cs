using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace JustAnEpicTest
{
    internal class Character
    {
        String path;
        public struct Pair
        {
            public int x { get; set; }
            public int y { get; set; }

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
        [JsonPropertyName("position")]
        public Pair position { get; set; }

        public String symbol;

        public Character(String symbol, Pair position, String path) {
            this.symbol = symbol;
            this.path = path;
            this.position = position;
        }
        [JsonConstructor]
        public Character(Pair position)
        {
            this.position = position;
        }
        public bool Move(Direction direction)
        {
            Pair newPosition = position;
            switch (direction)
            {
                case Direction.up:
                    newPosition.y--;
                    break;
                case Direction.down:
                    newPosition.y++;
                    break;
                case Direction.left:
                    newPosition.x--;
                    break;
                case Direction.right:
                    newPosition.x++;
                    break;
                case Direction.none:
                    break;
            }
            position = newPosition;
            return false;
        }

        public void SaveData(Character player)
        {
            List<Character> asd = new List<Character>();
            asd.Add(player);
            String json = JsonSerializer.Serialize(asd, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        public void LoadData()
        {
            if (!File.Exists(path))
            {
                SaveData(this);
                return;
            }
            String json = File.ReadAllText(path);
            List<Character> newPlayer = JsonSerializer.Deserialize<List<Character>>(json);
            position = newPlayer[0].position;
        }
    }
}
