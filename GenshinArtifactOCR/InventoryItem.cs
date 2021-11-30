using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace GenshinArtifactOCR
{
    class InventoryItem
    {
        public Tuple<string, string> piece;
        public Tuple<string, string, double> main;
        public Tuple<string, int> level;
        public List<Tuple<string, string, double>> subs;
        public Tuple<string, string> set;
        public Tuple<string, string> character;
        public bool locked = false;
        public int rarity = 0;

        public InventoryItem()
        {

        }

        public override string ToString()
        {
            string text = "";

            text += "Piece: ";
            if (piece != null)
                text += piece + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Main: ";
            if (main != null)
                text += main + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Rarity: " + rarity + Environment.NewLine;

            text += "Level: ";
            if (level != null)
                text += level + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Subs: ";
            if (subs != null)
            {
                text += subs.Count + Environment.NewLine;
                foreach (var sub in subs)
                {
                    text += sub + Environment.NewLine;
                }
            }
            else
                text += "Null-------" + Environment.NewLine;

            text += "Set: ";
            if (set != null)
                text += set + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Char: ";
            if (character != null)
                text += character + Environment.NewLine;
            else
                text += "Null" + Environment.NewLine;

            text += "Locked: " + locked + Environment.NewLine;





            return text;
        }

        public JObject toGOODArtifact()
        {
            JObject result = new JObject();

            if (set != null)
                result.Add("setKey", JToken.FromObject(set.Item2));
            result.Add("rarity", JToken.FromObject(rarity));
            if (level != null)
                result.Add("level", JToken.FromObject(level.Item2));
            if (piece != null)
                result.Add("slotKey", JToken.FromObject(piece.Item2));
            if (main != null)
                result.Add("mainStatKey", JToken.FromObject(main.Item2));
            if (subs != null)
            {
                JArray subsJArr = new JArray();
                foreach(Tuple<string, string, double> sub in subs)
                {
                    JObject subJObj = new JObject();
                    subJObj.Add("key", JToken.FromObject(sub.Item2));
                    subJObj.Add("value", JToken.FromObject(sub.Item3));
                    subsJArr.Add(subJObj);
                }
                result.Add("substats", subsJArr);
            }
            if (character != null)
                result.Add("location", JToken.FromObject(character.Item2));
            else
                result.Add("location", JToken.FromObject(""));

            result.Add("lock", JToken.FromObject(locked));
            return result;
        }

        public static JObject listToGOODArtifacts(List<InventoryItem> items, int minLevel, int maxLevel, int minRarity, int maxRarity)
        {
            JObject result = new JObject();
            JArray artifactJArr = new JArray();
            foreach (InventoryItem item in items)
            {
                if (item.level.Item2 >= minLevel && item.level.Item2 <= maxLevel && item.rarity >= minRarity && item.rarity <= maxRarity)
                    artifactJArr.Add(item.toGOODArtifact());
            }
            result.Add("artifacts", artifactJArr);
            return result;
        }
    }
}
