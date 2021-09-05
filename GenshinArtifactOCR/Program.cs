using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenshinArtifactOCR
{
    static class Program
    {


        private static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB", false);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory(GenshinArtifactOCR.appDir);
            Directory.CreateDirectory(GenshinArtifactOCR.appDir + @"\tessdata");
            Directory.CreateDirectory(GenshinArtifactOCR.appDir + @"\images");
            Directory.CreateDirectory(GenshinArtifactOCR.appDir + @"\filterdata");
            GenerateFilters();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GenshinArtifactOCR());
        }

        static void ReadMainStats(JArray mainStats)
        {
            foreach (JObject mainStat in mainStats)
            {
                foreach (string statName in mainStat["name"].ToObject<List<string>>())
                {

                    foreach (double statValue in mainStat["value"].ToObject<List<double>>())
                    {
                        string text = statName + statValue.ToString("N0", culture);
                        if (statName.Contains("%"))
                        {
                            text = statName + statValue.ToString("N1", culture);
                            text = text.Replace("%", "") + "%";
                        }
                        GenshinArtifactOCR.MainStats.Add(text);
                        //Console.WriteLine(text);
                    }
                }
            }
        }

        static void readSubstats(JArray substats)
        {
            foreach (JObject substat in substats)
            {
                foreach (string statName in substat["name"].ToObject<List<string>>())
                {
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
                        GenshinArtifactOCR.Substats.Add(text);
                        //Console.WriteLine(text);
                    }
                }
            }
        }

        static void readSets(JArray sets)
        {
            foreach (JObject set in sets)
            {
                foreach (string statName in set["name"].ToObject<List<string>>())
                {
                    GenshinArtifactOCR.Sets.Add(statName + ":");
                    for (int i = 0; i < 6; i++)
                    {
                        string text = statName + ":(" + i + ")";
                        GenshinArtifactOCR.Sets.Add(text);
                        //Console.WriteLine(text);
                    }
                }
            }
        }

        static void readCharacters(JArray characters)
        {
            foreach (JObject character in characters)
            {
                foreach (string statName in character["name"].ToObject<List<string>>())
                {
                    string text =  "Equipped: " + statName;
                    GenshinArtifactOCR.Characters.Add(text);
                    Console.WriteLine(text);
                }
            }
        }

        /// <summary>
        /// Generate all possible text to look for and assign to filter word lists
        /// </summary>
        static void GenerateFilters()
        {
            //Main stat filter
            JObject allJson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(GenshinArtifactOCR.appDir + @"\filterdata\ArtifactInfo.json"));
            foreach (var entry in allJson)
            {
                if (entry.Key == "MainStats")
                {
                    JArray mainStats = entry.Value.ToObject<JArray>();
                    ReadMainStats(mainStats);
                }

                if (entry.Key == "Substats")
                {
                    JArray substats = entry.Value.ToObject<JArray>();
                    readSubstats(substats);
                }

                if (entry.Key == "Sets")
                {
                    JArray sets = entry.Value.ToObject<JArray>();
                    readSets(sets);
                }

                if (entry.Key == "Characters")
                {
                    JArray characters = entry.Value.ToObject<JArray>();
                    readCharacters(characters);
                }

            }

            //Level filter
            for (int i = 0; i < 21; i++)
            {
                GenshinArtifactOCR.Levels.Add("+" + i);
            }
        }
    }
}
