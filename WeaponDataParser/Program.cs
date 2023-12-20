using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace WeaponDataParser // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting weapon parser");
            string basePath = @"C:\Users\Daniel\source\repos\D1firehail\AdeptiScanner-GI\AdeptiScanner GI\Weapons";//Environment.CurrentDirectory;
            Dictionary<string, List<double>> curveDict = GetGrowthCurves(Path.Combine(basePath, "expCurve.json"));

            JArray weapons = new JArray();
            Console.WriteLine("Parsing Bow");
            AddWeapons(weapons, Path.Combine(basePath, "Bow"), curveDict);
            Console.WriteLine("Parsing Catalyst");
            AddWeapons(weapons, Path.Combine(basePath, "Catalyst"), curveDict);
            Console.WriteLine("Parsing Claymore");
            AddWeapons(weapons, Path.Combine(basePath, "Claymore"), curveDict);
            Console.WriteLine("Parsing Polearm");
            AddWeapons(weapons, Path.Combine(basePath, "Polearm"), curveDict);
            Console.WriteLine("Parsing Sword");
            AddWeapons(weapons, Path.Combine(basePath, "Sword"), curveDict);

            Console.WriteLine("Saving");
            File.WriteAllText(Path.Combine(basePath, "weapons.json"), weapons.ToString(Formatting.Indented));
            File.WriteAllText(Path.Combine(basePath, "weapons_min.json"), weapons.ToString(Formatting.None));
        }

        static Dictionary<string, List<double>> GetGrowthCurves(string path)
        {
            JObject allCurveJson = new JObject();
            try
            {
                var tmp = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path));
                if (tmp != null )
                {
                    allCurveJson = tmp;
                } else
                {
                    throw new InvalidOperationException("Curve JSON returned null");
                }
            } catch (Exception e) 
            {
                Console.WriteLine("Failed to get curve JSON " + Environment.NewLine + e.Message);
                Environment.Exit(1);
            }

            var dict =  allCurveJson.ToObject<Dictionary<string, List<double>>>();

            if (dict != null )
            {
                return dict;
            } 
            else
            {
                Console.WriteLine("Failed to convert curve JSON to dict " + Environment.NewLine);
                Environment.Exit(1);
                //should be unreachable
                return new Dictionary<string, List<double>>();
            }
        }

        static void AddWeapons(JArray weapons, string categoryPath, Dictionary<string, List<double>> curveDict)
        {
            string[] items = Directory.GetDirectories(categoryPath);
            foreach (string itemPath in items)
            {
                string key = itemPath.Split(Path.DirectorySeparatorChar).Last();
                string dataPath = Path.Combine(itemPath, "data.json");
                JObject unparsed = new JObject();
                try
                {
                    var tmp = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(dataPath));
                    if (tmp != null)
                    {
                        unparsed = tmp;
                    }
                    else
                    {
                        throw new InvalidOperationException("Weapon JSON "  + key + " returned null");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get Weapon JSON "  + dataPath + Environment.NewLine + e.Message);
                    Environment.Exit(1);
                }

                int rarity = unparsed["rarity"].ToObject<int>();

                JObject mainStat = unparsed["mainStat"].ToObject<JObject>();

                double baseAtk = mainStat["base"].ToObject<double>();

                string curveKey = mainStat["curve"].ToObject<string>();

                List<double> ascBonus = unparsed["ascensionBonus"].ToObject<JObject>()["atk"].ToObject<List<double>>();

                List<double> curve = curveDict[curveKey];

                JArray statLevelCombo = new JArray();

                for (int i = 1; i < Math.Min(curve.Count, 91); i++)
                {
                    bool isAscPoint = i == 20 || i == 40 || i == 50 || i == 60 || i == 70 || i == 80;
                    int ascension = 0;
                    if (i >= 80)
                    {
                        ascension = 6;
                    } 
                    else if (i >= 70)
                    {
                        ascension = 5;
                    }
                    else if (i >= 60)
                    {
                        ascension = 4;
                    }
                    else if (i >= 50)
                    {
                        ascension = 3;
                    }
                    else if (i >= 40)
                    {
                        ascension = 2;
                    }
                    else if (i >= 20)
                    {
                        ascension = 1;
                    }

                    double attack = baseAtk * curve[i] + ascBonus[Math.Min(ascension, ascBonus.Count - 1)];
                    JObject tmp = new JObject
                    {
                        { "level", i },
                        { "baseAtk", attack.ToString("F0") },
                        { "ascension", ascension }
                    };
                    statLevelCombo.Add(tmp);

                    if (isAscPoint)
                    {
                        //add pre-ascension stats
                        attack = baseAtk * curve[i] + ascBonus[Math.Min(ascension - 1, ascBonus.Count - 1)];
                        tmp = new JObject
                        {
                            { "level", i },
                            { "baseAtk", attack.ToString("F0") },
                            { "ascension", ascension - 1 }
                        };
                        statLevelCombo.Add(tmp);
                    }

                }

                JObject wep = new JObject
                {
                    { "key", key },
                    { "rarity", rarity },
                    { "stats", statLevelCombo }
                };
                weapons.Add(wep);
            }
        }
    }
}