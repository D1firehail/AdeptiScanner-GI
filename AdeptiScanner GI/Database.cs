using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdeptiScanner_GI
{
    #region IParsable stuff
    public interface IParsableData
    {
        string GetPlainText();
    }

    public readonly record struct SimpleParsable(string Text) : IParsableData
    {
        public string GetPlainText() => Text;
    }

    public readonly record struct PieceData(string Text, string StatKey) : IParsableData
    {
        public string GetPlainText() => Text;
    }

    public readonly record struct CharacterNameData(string Text, string Key) : IParsableData
    {
        public string GetPlainText() => Text;
    }

    public readonly record struct ArtifactLevelData(string Text, int Key) : IParsableData
    {
        public string GetPlainText() => Text;
    }

    public readonly record struct ArtifactMainStatData(string Text, string StatKey, double StatValue, int Level) : IParsableData
    {
        public string GetPlainText() => Text;
    }

    public readonly record struct ArtifactSubStatData(string Text, string StatKey, double StatValue) : IParsableData
    {
        public string GetPlainText() => Text;
    }

    public readonly record struct ArtifactSetData(string Text, string Key) : IParsableData
    {
        public string GetPlainText() => Text;
    }

    public readonly record struct WeaponNameData(string Key, int Rarity) : IParsableData
    {
        public string GetPlainText() => Key; // weapons currently only parse the GOOD name and assumes it's close enough to the english name
    }

    public readonly record struct WeaponLevelAndAscensionData(string BaseAtk, int Level, int Ascension) : IParsableData
    {
        public string GetPlainText() => BaseAtk;
    }

    #endregion

    class Database
    {
        private static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB", false);
        public static string appDir = Path.Join(Application.StartupPath, "ScannerFiles");
        public static string appdataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AdeptiScanner");
        public static string programVersion = "2.1.0";
        public static string dataVersion = "X.XX";
        //These get filled on startup by other file
        public static List<PieceData> Pieces = new List<PieceData>();
        public static List<CharacterNameData> Characters = new List<CharacterNameData>();
        public static List<ArtifactLevelData> ArtifactLevels = new List<ArtifactLevelData>();
        public static Database[] rarityData = new Database[5];
        public static List<WeaponNameData> WeaponNames = new List<WeaponNameData>();
        public static Dictionary<WeaponNameData, List<WeaponLevelAndAscensionData>> WeaponLevels = new Dictionary<WeaponNameData, List<WeaponLevelAndAscensionData>>();

        public static Dictionary<int, string> CharacterNames = new Dictionary<int, string>();
        public static Dictionary<string, string> SkillTypes = new Dictionary<string, string>();



        public List<ArtifactMainStatData> MainStats = new List<ArtifactMainStatData>();
        public List<ArtifactSubStatData> Substats = new List<ArtifactSubStatData>();
        public List<ArtifactSetData> Sets = new List<ArtifactSetData>();

        public Database()
        {

        }


        /// <summary>
        /// Get Levenshtein Distance between two strings, taken from WFInfo and slightly modified
        /// </summary>
        /// <param name="s">One of the words to compare</param>
        /// <param name="t">Second word to compare</param>
        /// <returns>Levenshtein distance between <paramref name="s"/> and <paramref name="t"/>, after some filtering</returns>
        public static int LevenshteinDistance(string s, string t)
        {
            // Levenshtein Distance determines how many character changes it takes to form a known result
            // For more info see: https://en.wikipedia.org/wiki/Levenshtein_distance
            s = s.ToLower();
            t = t.ToLower();
            s = Regex.Replace(s, @"[+,.: ]", "");
            t = Regex.Replace(t, @"[+,.: ]", "");
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0 || m == 0)
                return n + m;

            d[0, 0] = 0;

            int count = 0;
            for (int i = 1; i <= n; i++)
                d[i, 0] = (s[i - 1] == ' ' ? count : ++count);

            count = 0;
            for (int j = 1; j <= m; j++)
                d[0, j] = (t[j - 1] == ' ' ? count : ++count);

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    // deletion of s
                    int opt1 = d[i - 1, j];
                    if (s[i - 1] != ' ')
                        opt1++;

                    // deletion of t
                    int opt2 = d[i, j - 1];
                    if (t[j - 1] != ' ')
                        opt2++;

                    // swapping s to t
                    int opt3 = d[i - 1, j - 1];
                    if (t[j - 1] != s[i - 1])
                        opt3++;
                    d[i, j] = Math.Min(Math.Min(opt1, opt2), opt3);
                }



            return d[n, m];
        }

        /// <summary>
        /// Get closest match according to Levenshtein Distance, taken from WFInfo but slightly modified
        /// </summary>
        /// <param name="rawText">Word to find match for</param>
        /// <param name="validText">List of words to match against</param>
        /// <param name="dist">Levenshtein distance to closest match</param>
        /// <returns>Closest matching word</returns>
        public static string FindClosestMatch<T>(string rawText, List<T> validText, out T? result, out int dist) where T : struct, IParsableData
        {
            string lowest = "ERROR";
            dist = 9999;
            result = null;

            for (int i = 0; i < validText.Count; i++)
            {
                string validWord = validText[i].GetPlainText();
                int val = LevenshteinDistance(validWord, rawText);
                if (val < dist)
                {
                    result = validText[i];
                    dist = val;
                    lowest = validWord;
                }
            }
            return lowest;
        }


        void ReadMainStats(JArray mainStats)
        {
            foreach (JObject mainStat in mainStats)
            {
                foreach (KeyValuePair<string, JToken> statNameTup in mainStat["name"].ToObject<JObject>())
                {
                    string statName = statNameTup.Key;
                    string statKey = statNameTup.Value.ToObject<string>();

                    List<double> statValues = mainStat["value"].ToObject<List<double>>();
                    for (int i = 0; i < statValues.Count; i++) 
                    { 
                        double statValue = statValues[i];
                        string text = statName + statValue.ToString("N0", culture);
                        if (statName.Contains("%"))
                        {
                            text = statName + statValue.ToString("N1", culture);
                            text = text.Replace("%", "") + "%";
                        }
                        MainStats.Add( new ArtifactMainStatData(text, statKey, statValue, i));
                        //Console.WriteLine(text + " ---- " + statValue);
                    }
                }
            }
        }

        void readSubstats(JArray substats)
        {
            foreach (JObject substat in substats)
            {
                foreach (KeyValuePair<string, JToken> statNameTup in substat["name"].ToObject<JObject>())
                {
                    string statName = statNameTup.Key;
                    string statKey = statNameTup.Value.ToObject<string>();
                    List<int> baserolls = new List<int>();
                    List<int> rolls = new List<int>();
                    foreach (double statValue in substat["rolls"].ToObject<List<double>>())
                    {
                        baserolls.Add((int)(statValue * 100));
                        rolls.Add((int)(statValue * 100));

                    }

                    int start = 0;
                    int stop = rolls.Count;
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = start; j < stop; j++)
                        {
                            foreach (int value in baserolls)
                            {
                                int tmp = rolls[j] + value;
                                if (!rolls.Contains(tmp))
                                    rolls.Add(tmp);
                            }
                        }
                        start = stop;
                        stop = rolls.Count;
                    }
                    foreach (int value_int in rolls)
                    {
                        double value = value_int / 100.0 + 0.001;
                        string text = statName + value.ToString("N0", culture);
                        if (statName.Contains("%"))
                        {
                            text = statName + value.ToString("N1", culture);
                            value = Math.Round(value, 1);
                            text = text.Replace("%", "") + "%";
                        } else
                        {
                            value = Math.Round(value, 0);
                        }
                        Substats.Add( new ArtifactSubStatData(text, statKey, value));
                    }
                }
            }
        }

        void readSets(JArray sets)
        {
            foreach (JObject set in sets)
            {
                foreach (KeyValuePair<string, JToken> statNameTup in set["name"].ToObject<JObject>())
                {
                    string statName = statNameTup.Key;
                    string statKey = statNameTup.Value.ToObject<string>();
                    string text = statName + "";
                    Sets.Add(new ArtifactSetData(text, statKey));
                }
            }
        }

        static void readCharacters(JArray characters)
        {
            foreach (JObject character in characters)
            {
                foreach (KeyValuePair<string, JToken> statNameTup in character["name"].ToObject<JObject>())
                {
                    string statName = statNameTup.Key;
                    string statKey = statNameTup.Value.ToObject<string>();
                    string text = "Equipped: " + statName;
                    Characters.Add(new CharacterNameData(text, statKey));
                    //Console.WriteLine(text);
                }
            }
        }

        static void readPieces(JArray pieces)
        {
            foreach (JObject piece in pieces)
            {
                foreach (KeyValuePair<string, JToken> statNameTup in piece["name"].ToObject<JObject>())
                {
                    string statName = statNameTup.Key;
                    string statKey = statNameTup.Value.ToObject<string>();
                    string text = statName;
                    Pieces.Add(new PieceData(text, statKey));
                    //Console.WriteLine(text);
                }
            }
        }

        static void readEnka(JArray enka)
        {
            foreach (JObject data in enka)
            {
                if (data["name"].ToObject<string>().Equals("charNames") )
                {
                    CharacterNames = data["data"].ToObject<Dictionary<int, string>>();
                }

                if (data["name"].ToObject<string>().Equals("skillTypes"))
                {
                    SkillTypes = data["data"].ToObject<Dictionary<string, string>>();
                }
            }
        }

        static void readWeapons(JArray weapons)
        {
            foreach (JObject weapon in weapons)
            {
                string name = weapon["key"].ToObject<string>();
                int rarity = weapon["rarity"].ToObject<int>();
                WeaponNameData nameData = new WeaponNameData(name, rarity);
                WeaponNames.Add(nameData);

                List<JObject> stats = weapon["stats"].ToObject<List<JObject>>();
                List<WeaponLevelAndAscensionData> levelsAndAscensions = new List<WeaponLevelAndAscensionData>();

                foreach (var statPoint in stats)
                {
                    string baseAtk = statPoint["baseAtk"].ToObject<string>();
                    int level = statPoint["level"].ToObject<int>();
                    int ascension = statPoint["ascension"].ToObject<int>();
                    levelsAndAscensions.Add( new WeaponLevelAndAscensionData(baseAtk, level, ascension) );
                }
                WeaponLevels[nameData] = levelsAndAscensions;
            }
        }

        /// <summary>
        /// Generate all possible text to look for and assign to filter word lists
        /// </summary>
        public static void GenerateFilters()
        {
            for (int i = 0; i < rarityData.Length; i++)
            {
                rarityData[i] = new Database();
            }
            //Main stat filter
            JObject allJson = new JObject();
            try
            {
                allJson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Join(appDir, "ArtifactInfo.json")));
            } 
            catch (Exception e)
            {
                MessageBox.Show("Error trying to access ArtifactInfo file" + Environment.NewLine + Environment.NewLine +
                    "Exact error:" + Environment.NewLine + e.ToString(),

                    "Scanner could not start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            if (allJson.TryGetValue("DataVersion", out JToken ver)) {
                dataVersion = ver.ToObject<string>();
                allJson.Remove("DataVersion");
            }
            foreach (KeyValuePair<string, JToken> entry in allJson)
            {
                JArray entry_arr = entry.Value.ToObject<JArray>();
                if (entry.Key == "ArtifactTiers")
                {
                    foreach (JObject rarityTier in entry_arr)
                    {
                        int rarity = rarityTier["rarity"].ToObject<int>();
                        JObject tierData = rarityTier["data"].ToObject<JObject>();

                        foreach (KeyValuePair<string, JToken> rarityEntry in tierData)
                        {
                            JArray rarityEntry_arr = rarityEntry.Value.ToObject<JArray>();
                            if (rarityEntry.Key == "MainStats")
                            {
                                rarityData[rarity - 1].ReadMainStats(rarityEntry_arr);
                            }

                            if (rarityEntry.Key == "Substats")
                            {
                                rarityData[rarity - 1].readSubstats(rarityEntry_arr);
                            }

                            if (rarityEntry.Key == "Sets")
                            {
                                rarityData[rarity - 1].readSets(rarityEntry_arr);
                            }
                        }
                    }

                }

                if (entry.Key == "Characters")
                {
                    readCharacters(entry_arr);
                }

                if (entry.Key == "Pieces")
                {
                    readPieces(entry_arr);
                }

                if (entry.Key == "Weapons")
                {
                    readWeapons(entry_arr);
                }

                if (entry.Key == "Enka")
                {
                    readEnka(entry_arr);
                }

            }

            //Level filter
            for (int i = 0; i < 21; i++)
            {
                string text = "+" + i;
                int statValue = i;
                ArtifactLevels.Add(new ArtifactLevelData(text, statValue));
            }
        }

        public static void SetCharacterName(string displayName, string GOODName)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].Key == GOODName)
                {
                    Characters.RemoveAt(i);
                    Characters.Insert(i, new CharacterNameData("Equipped: " + displayName, GOODName));
                    break;
                }
            }
        }

        public static bool artifactInvalid(int rarity, Artifact item)
        {
            return rarity < 0 || rarity > 5 || item.piece == null || item.main == null || item.level == null || item.subs == null || item.set == null
                || (rarity == 1 && (item.level.Value.Key > 4 || item.subs.Count > 1)) 
                || (rarity == 2 && (item.level.Value.Key > 4 || item.subs.Count > 2)) 
                || (rarity == 3 && (item.level.Value.Key > 12 || item.subs.Count > 4 || item.subs.Count < 1))
                || (rarity == 4 && (item.level.Value.Key > 16 || item.subs.Count > 4 || item.subs.Count < 2)) 
                || (rarity == 5 && (item.level.Value.Key > 20 || item.subs.Count > 4 || item.subs.Count < 3));
        }

        public static bool weaponInvalid(Weapon item)
        {
            return item.level == null || item.name == null || item.level.Value.Level > 90 
                || (item.name.Value.Rarity < 3 && item.level.Value.Level > 70) 
                || (item.name.Value.Rarity >= 3 && item.refinement == null);
        }
    }

}
