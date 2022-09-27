using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AdeptiScanner_GI
{
    public class InventoryItem
    {
        public Tuple<string, string> piece;
        public Tuple<string, string, double, int> main;
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

            text += "Level (deprecated): ";
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
            if (main != null)
                result.Add("level", JToken.FromObject(main.Item4));
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

            /*if (level != null && main != null && level.Item2 != main.Item4)
            {
                Console.WriteLine("Read level: " + level.Item2 + ", mainstat level: " + main.Item4);
                Console.WriteLine(this.ToString());
                
            }*/
            return result;
        }

        public static InventoryItem fromGOODArtifact(JObject GOODArtifact)
        {
            InventoryItem res = new InventoryItem();
            if (GOODArtifact.ContainsKey("rarity"))
            {
                res.rarity = GOODArtifact["rarity"].ToObject<int>();
            } else
            {
                return null;
            }
            Database rarityDb = Database.rarityData[res.rarity - 1];
            if (GOODArtifact.ContainsKey("setKey"))
            {
                string setKey = GOODArtifact["setKey"].ToObject<string>();
                for (int i = 0; i < rarityDb.Sets_trans.Count; i++)
                {
                    if (rarityDb.Sets_trans[i].Item2 == setKey)
                    {
                        res.set = rarityDb.Sets_trans[i];
                        break;
                    }
                }
            }
            if (GOODArtifact.ContainsKey("slotKey"))
            {
                string slotKey = GOODArtifact["slotKey"].ToObject<string>();
                for (int i = 0; i < Database.Pieces_trans.Count; i++)
                {
                    if (Database.Pieces_trans[i].Item2 == slotKey)
                    {
                        res.piece = Database.Pieces_trans[i];
                        break;
                    }
                }
            }
            if (GOODArtifact.ContainsKey("mainStatKey") && GOODArtifact.ContainsKey("level"))
            {
                string mainStatKey = GOODArtifact["mainStatKey"].ToObject<string>();
                int levelKey = GOODArtifact["level"].ToObject<int>();

                for (int i = 0; i < rarityDb.MainStats_trans.Count; i++)
                {
                    if (rarityDb.MainStats_trans[i].Item2 == mainStatKey && rarityDb.MainStats_trans[i].Item4 == levelKey)
                    {
                        res.main = rarityDb.MainStats_trans[i];
                        break;
                    }
                }
                for (int i = 0; i < Database.Levels_trans.Count; i++)
                {
                    if (Database.Levels_trans[i].Item2 == levelKey)
                    {
                        res.level = Database.Levels_trans[i];
                        break;
                    }
                }
            }
            if (GOODArtifact.ContainsKey("location"))
            {
                string locationKey = GOODArtifact["location"].ToObject<string>();

                for (int i = 0; i < Database.Characters_trans.Count; i++)
                {
                    if (Database.Characters_trans[i].Item2 == locationKey)
                    {
                        res.character = Database.Characters_trans[i];
                        break;
                    }
                }
            }
            if (GOODArtifact.ContainsKey("lock"))
            {
                res.locked = GOODArtifact["lock"].ToObject<bool>();
            }
            if (GOODArtifact.ContainsKey("substats"))
            {
                JArray substats = GOODArtifact["substats"].ToObject<JArray>();
                res.subs = new List<Tuple<string, string, double>>();
                foreach (JObject sub in substats)
                {
                    if (sub.ContainsKey("key") && sub.ContainsKey("value") )
                    {
                        string statKey = sub["key"].ToObject<string>();
                        double statVal = sub["value"].ToObject<double>();
                        for (int i = 0; i < rarityDb.Substats_trans.Count; i++)
                        {
                            if (rarityDb.Substats_trans[i].Item2 == statKey && rarityDb.Substats_trans[i].Item3 - statVal < 0.099)
                            {
                                res.subs.Add(rarityDb.Substats_trans[i]);
                                break;
                            }
                        }
                    }
                }
            }


            return res;
        }

        public static JObject listToGOODArtifacts(List<InventoryItem> items, int minLevel, int maxLevel, int minRarity, int maxRarity, bool exportAllEquipped)
        {
            JObject result = new JObject();
            JArray artifactJArr = new JArray();
            foreach (InventoryItem item in items)
            {
                if ( (exportAllEquipped && item.character != null) || (item.level.Item2 >= minLevel && item.level.Item2 <= maxLevel && item.rarity >= minRarity && item.rarity <= maxRarity) )
                    artifactJArr.Add(item.toGOODArtifact());
            }
            result.Add("artifacts", artifactJArr);
            return result;
        }
    }
}
