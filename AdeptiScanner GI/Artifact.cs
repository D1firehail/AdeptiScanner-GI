using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdeptiScanner_GI
{
    public class Artifact
    {
        public PieceData? piece;
        public ArtifactMainStatData? main;
        public ArtifactLevelData? level; // unreliable
        public List<ArtifactSubStatData> subs;
        public ArtifactSetData? set;
        public CharacterNameData? character;
        public bool locked = false;
        public bool astralMark = false;
        public bool elixirCrafted = false;
        public int rarity = 0;

        // not used internally, just to not lose data between import/export
        public Dictionary<string, double> importedInitialValues;
        public int? importedTotalRolls;

        public Artifact()
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
            text += "Astral Mark: " + astralMark + Environment.NewLine;
            text += "Elixir Crafted: " + elixirCrafted + Environment.NewLine;





            return text;
        }

        public JObject toGOODArtifact(bool includeLocation = true)
        {
            JObject result = new JObject();

            if (set != null)
                result.Add("setKey", JToken.FromObject(set.Value.Key));
            result.Add("rarity", JToken.FromObject(rarity));
            if (main != null)
                result.Add("level", JToken.FromObject(main.Value.Level));
            if (piece != null)
                result.Add("slotKey", JToken.FromObject(piece.Value.StatKey));
            if (main != null)
                result.Add("mainStatKey", JToken.FromObject(main.Value.StatKey));
            int? totalRollCount = null;
            if (subs != null)
            {
                JArray subsJArr = new JArray();
                JArray unactivatedSubsJArr = new JArray();
                foreach (ArtifactSubStatData sub in subs)
                {
                    JObject subJObj = new JObject();
                    subJObj.Add("key", JToken.FromObject(sub.StatKey));
                    subJObj.Add("value", JToken.FromObject(sub.StatValue));

                    if (importedInitialValues != null && importedInitialValues.TryGetValue(sub.StatKey, out double dictInitialRoll))
                    {
                        // prefer imported initial roll over own attempt
                        subJObj.Add("initialValue", JToken.FromObject(dictInitialRoll));
                    } else if (sub.MaxRolls == 1)
                    {
                        // could maybe detect results requiring all rolls to be the same
                        // like 7.8 CR requiring 2 x 3.9
                        // or or 5.4 CR requiring 2 x 2.7
                        subJObj.Add("initialValue", JToken.FromObject(sub.StatValue));
                    }

                    if (sub.IsUnactivated)
                    {
                        unactivatedSubsJArr.Add(subJObj);
                    } else
                    {
                        subsJArr.Add(subJObj);
                    }
                }

                if (main != null)
                {
                    int minRollCount = subs.Where(x => !x.IsUnactivated).Sum(x => x.MinRolls);
                    int maxRollCount = subs.Where(x => !x.IsUnactivated).Sum(x => x.MaxRolls);

                    int maxPossibleRollCount = GetMaxRollCount(rarity, main.Value.Level);
                    int minPossibleRollCount = GetMinRollCount(rarity, main.Value.Level);

                    if (minRollCount == maxRollCount)
                    {
                        // unambiguous
                        totalRollCount = minRollCount;
                    } else if (maxRollCount > maxPossibleRollCount && minRollCount == maxPossibleRollCount)
                    {
                        // ambiguity solved because only 1 option is possible
                        totalRollCount = minRollCount;
                    } else if (minRollCount < minPossibleRollCount && maxRollCount == minPossibleRollCount)
                    {
                        // ambiguity solved because only 1 option is possible
                        totalRollCount = minRollCount;
                    }
                }

                result.Add("substats", subsJArr);
                result.Add("unactivatedSubstats", unactivatedSubsJArr);
            }

            if (importedTotalRolls.HasValue)
            {
                // prefer imported roll count over own attempt
                totalRollCount = importedTotalRolls.Value;
            }

            if (totalRollCount.HasValue)
            {
                result.Add("totalRolls", JToken.FromObject(totalRollCount.Value));
            }

            if (includeLocation)
            {
                if (character != null)
                    result.Add("location", JToken.FromObject(character.Value.Key));
                else
                    result.Add("location", JToken.FromObject(""));
            }

            result.Add("lock", JToken.FromObject(locked));
            result.Add("astralMark", JToken.FromObject(astralMark));
            result.Add("elixirCrafted", JToken.FromObject(elixirCrafted));

            /*if (level != null && main != null && level.Item2 != main.Item4)
            {
                Console.WriteLine("Read level: " + level.Item2 + ", mainstat level: " + main.Item4);
                Console.WriteLine(this.ToString());
                
            }*/
            return result;
        }

        public static Artifact fromGOODArtifact(JObject GOODArtifact)
        {
            Artifact res = new Artifact();
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
                for (int i = 0; i < rarityDb.Sets.Count; i++)
                {
                    if (rarityDb.Sets[i].Key == setKey)
                    {
                        res.set = rarityDb.Sets[i];
                        break;
                    }
                }
            }
            if (GOODArtifact.ContainsKey("slotKey"))
            {
                string slotKey = GOODArtifact["slotKey"].ToObject<string>();
                for (int i = 0; i < Database.Pieces.Count; i++)
                {
                    if (Database.Pieces[i].StatKey == slotKey)
                    {
                        res.piece = Database.Pieces[i];
                        break;
                    }
                }
            }
            if (GOODArtifact.ContainsKey("mainStatKey") && GOODArtifact.ContainsKey("level"))
            {
                string mainStatKey = GOODArtifact["mainStatKey"].ToObject<string>();
                int levelKey = GOODArtifact["level"].ToObject<int>();

                for (int i = 0; i < rarityDb.MainStats.Count; i++)
                {
                    if (rarityDb.MainStats[i].StatKey == mainStatKey && rarityDb.MainStats[i].Level == levelKey)
                    {
                        res.main = rarityDb.MainStats[i];
                        break;
                    }
                }
                for (int i = 0; i < Database.ArtifactLevels.Count; i++)
                {
                    if (Database.ArtifactLevels[i].Key == levelKey)
                    {
                        res.level = Database.ArtifactLevels[i];
                        break;
                    }
                }
            }
            if (GOODArtifact.ContainsKey("location"))
            {
                string locationKey = GOODArtifact["location"].ToObject<string>();

                for (int i = 0; i < Database.Characters.Count; i++)
                {
                    if (Database.Characters[i].Key == locationKey)
                    {
                        res.character = Database.Characters[i];
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
                foreach (JObject sub in substats)
                {
                    res.ImportSubstat(sub, rarityDb, false);
                }
            }

            if (GOODArtifact.ContainsKey("unactivatedSubstats"))
            {
                JArray substats = GOODArtifact["unactivatedSubstats"].ToObject<JArray>();
                foreach (JObject sub in substats)
                {
                    res.ImportSubstat(sub, rarityDb, true);
                }
            }

            if (GOODArtifact.ContainsKey("astralMark"))
            {
                res.astralMark = GOODArtifact["astralMark"].ToObject<bool>();
            }

            if (GOODArtifact.ContainsKey("elixirCrafted"))
            {
                res.elixirCrafted = GOODArtifact["elixirCrafted"].ToObject<bool>();
            }

            if (GOODArtifact.ContainsKey("totalRolls"))
            {
                res.importedTotalRolls = GOODArtifact["totalRolls"].ToObject<int>();
            }

            if (res.IsInvalid())
            {
                ScannerForm.INSTANCE.AppendStatusText("Failed to parse artifact: " + GOODArtifact.ToString(Newtonsoft.Json.Formatting.None) + Environment.NewLine, false);
                return null;
            } 
            else
            {
                return res;
            }
        }

        private void ImportSubstat(JObject sub, Database rarityDb, bool isUnactivated)
        {
            if (subs == null)
            {
                subs = new List<ArtifactSubStatData>();
            }

            if (importedInitialValues == null)
            {
                importedInitialValues = new();
            }

            if (sub.ContainsKey("key") && sub.ContainsKey("value"))
            {
                string statKey = sub["key"].ToObject<string>();
                double statVal = sub["value"].ToObject<double>();
                for (int i = 0; i < rarityDb.Substats.Count; i++)
                {
                    if (rarityDb.Substats[i].StatKey == statKey && Math.Abs(rarityDb.Substats[i].StatValue - statVal) < 0.099 && rarityDb.Substats[i].IsUnactivated == isUnactivated)
                    {
                        subs.Add(rarityDb.Substats[i]);
                        break;
                    }
                }

                if (sub.ContainsKey("initialValue"))
                {
                    double initialValue = sub["value"].ToObject<double>();
                    importedInitialValues[statKey] = initialValue;
                }
            }
        }

        public static JObject listToGOODArtifacts(List<Artifact> items, int minLevel, int maxLevel, int minRarity, int maxRarity, bool exportAllEquipped, bool exportEquipStatus)
        {
            JObject result = new JObject();
            JArray artifactJArr = new JArray();
            foreach (Artifact item in items)
            {
                bool add = false;
                if (exportAllEquipped && item.character != null)
                {
                    add = true;
                }
                    
                if (item.main.HasValue && item.main.Value.Level >= minLevel && item.main.Value.Level <= maxLevel
                    && item.rarity >= minRarity && item.rarity <= maxRarity)
                {
                    add = true;
                }

                if (add)
                { 
                    artifactJArr.Add(item.toGOODArtifact(exportEquipStatus)); 
                }
            }
            result.Add("artifacts", artifactJArr);
            return result;
        }

        private int GetMaxRollCount(int rarity, int level)
        {
            return Math.Clamp(rarity - 1, 0, 4) + level / 4;
        }

        private int GetMinRollCount(int rarity, int level)
        {
            if (rarity == 0)
            {
                return GetMaxRollCount(rarity, level);
            }

            return GetMaxRollCount(rarity, level) - 1;
        }

        public bool IsInvalid()
        {
            // null on mandatory values
            if (rarity < 0 || rarity > 5 || piece == null || main == null || subs == null || set == null)
            {
                return true;
            }

            // basic rarity-based level/substat count 
            if ((rarity == 1 && (main.Value.Level > 4 || subs.Count > 1))
                || (rarity == 2 && (main.Value.Level > 4 || subs.Count > 2))
                || (rarity == 3 && (main.Value.Level > 12 || subs.Count > 4 || subs.Count < 1))
                || (rarity == 4 && (main.Value.Level > 16 || subs.Count > 4 || subs.Count < 2))
                || (rarity == 5 && (main.Value.Level > 20 || subs.Count > 4 || subs.Count < 3)))
            {
                return true;
            }

            int minRollCount = subs.Where(x => !x.IsUnactivated).Sum(x => x.MinRolls);
            int maxRollCount = subs.Where(x => !x.IsUnactivated).Sum(x => x.MaxRolls);

            int maxPossibleRollCount = GetMaxRollCount(rarity, main.Value.Level);
            int minPossibleRollCount = GetMinRollCount(rarity, main.Value.Level);

            // total roll count
            if (maxRollCount < minPossibleRollCount || minRollCount > maxPossibleRollCount)
            {
                return true;
            }

            if (importedTotalRolls.HasValue && (importedTotalRolls.Value > maxPossibleRollCount || importedTotalRolls.Value < minPossibleRollCount))
            {
                return true;
            }

            bool hasPassedUnactivatedSub = false;
            foreach (var sub in subs) 
            {
                if (sub.IsUnactivated)
                {
                    hasPassedUnactivatedSub = true;
                }
                else if (hasPassedUnactivatedSub)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
