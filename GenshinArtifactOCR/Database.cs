using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace GenshinArtifactOCR
{
    class Database
    {
        private static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB", false);
        public static string appDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GenshinArtifactOCR";
        //These get filled on startup by other file
        public static List<string> Pieces = new List<string>();
        public static List<string> MainStats = new List<string>();
        public static List<string> Levels = new List<string>();
        public static List<string> Substats = new List<string>();
        public static List<string> Sets = new List<string>();
        public static List<string> Characters = new List<string>();
        public static List<Tuple<string, string>> Pieces_trans = new List<Tuple<string, string>>();
        public static List<Tuple<string, string, double>> MainStats_trans = new List<Tuple<string, string, double>>();
        public static List<Tuple<string, int>> Levels_trans = new List<Tuple<string, int>>();
        public static List<Tuple<string, string, double>> Substats_trans = new List<Tuple<string, string, double>>();
        public static List<Tuple<string, string>> Sets_trans = new List<Tuple<string, string>>();
        public static List<Tuple<string, string>> Characters_trans = new List<Tuple<string, string>>();



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
        public static string FindClosestMatch(string rawText, List<string> validText, out int index, out int dist)
        {
            string lowest = "ERROR";
            dist = 9999;
            index = 0;

            for (int i = 0; i < validText.Count; i++)
            {
                string validWord = validText[i];
                int val = LevenshteinDistance(validWord, rawText);
                if (val < dist)
                {
                    index = i;
                    dist = val;
                    lowest = validWord;
                }
            }
            return lowest;
        }


        static void ReadMainStats(JArray mainStats)
        {
            foreach (JObject mainStat in mainStats)
            {
                foreach (KeyValuePair<string, JToken> statNameTup in mainStat["name"].ToObject<JObject>())
                {
                    string statName = statNameTup.Key;
                    string statKey = statNameTup.Value.ToObject<string>();

                    foreach (double statValue in mainStat["value"].ToObject<List<double>>())
                    {
                        string text = statName + statValue.ToString("N0", culture);
                        if (statName.Contains("%"))
                        {
                            text = statName + statValue.ToString("N1", culture);
                            text = text.Replace("%", "") + "%";
                        }
                        MainStats.Add(text);
                        MainStats_trans.Add(Tuple.Create(text, statKey, statValue));
                        //Console.WriteLine(text);
                    }
                }
            }
        }

        static void readSubstats(JArray substats)
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
                    for (int i = 0; i < 4; i++)
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
                        double value = value_int / 100.0;
                        string text = statName + value.ToString("N0", culture);
                        if (statName.Contains("%"))
                        {
                            text = statName + value.ToString("N1", culture);
                            text = text.Replace("%", "") + "%";
                        }
                        Substats.Add(text);
                        Substats_trans.Add(Tuple.Create(text, statKey, value));
                        //Console.WriteLine(text);
                    }
                }
            }
        }

        static void readSets(JArray sets)
        {
            foreach (JObject set in sets)
            {
                foreach (KeyValuePair<string, JToken> statNameTup in set["name"].ToObject<JObject>())
                {
                    string statName = statNameTup.Key;
                    string statKey = statNameTup.Value.ToObject<string>();
                    string text = statName + "";
                    Sets.Add(text);
                    Sets_trans.Add(Tuple.Create(text, statKey));
                    for (int i = 0; i < 6; i++)
                    {
                        text = statName + ":(" + i + ")";
                        Sets.Add(text);
                        Sets_trans.Add(Tuple.Create(text, statKey));
                        //Console.WriteLine(text);
                    }
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
                    Characters.Add(text);
                    Characters_trans.Add(Tuple.Create(text, statKey));
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
                    Pieces.Add(text);
                    Pieces_trans.Add(Tuple.Create(text, statKey));
                    //Console.WriteLine(text);
                }
            }
        }

        /// <summary>
        /// Generate all possible text to look for and assign to filter word lists
        /// </summary>
        public static void GenerateFilters()
        {
            //Main stat filter
            JObject allJson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(appDir + @"\ArtifactInfo.json"));
            foreach (KeyValuePair<string, JToken> entry in allJson)
            {
                JArray entry_arr = entry.Value.ToObject<JArray>();
                if (entry.Key == "MainStats")
                {
                    ReadMainStats(entry_arr);
                }

                if (entry.Key == "Substats")
                {
                    readSubstats(entry_arr);
                }

                if (entry.Key == "Sets")
                {
                    readSets(entry_arr);
                }

                if (entry.Key == "Characters")
                {
                    readCharacters(entry_arr);
                }

                if (entry.Key == "Pieces")
                {
                    readPieces(entry_arr);
                }

            }

            //Level filter
            for (int i = 0; i < 21; i++)
            {
                string text = "+" + i;
                int statValue = i;
                Levels.Add(text);
                Levels_trans.Add(Tuple.Create(text, statValue));
            }
        }
    }
}
