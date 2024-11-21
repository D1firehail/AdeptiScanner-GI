using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace WeaponDataParser // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting weapon parser");

            GetRepoPaths(out string genshinOptimizerRepoPath, out string adeptiScannerRepoPath);

            string weaponPath = Path.Combine(genshinOptimizerRepoPath, @"libs\gi\stats\Data\Weapons");
            string translationPath = Path.Combine(genshinOptimizerRepoPath, @"libs\gi\dm-localization\assets\locales\en");

            string scannerFilesPath = Path.Combine(adeptiScannerRepoPath, @"AdeptiScanner GI\ScannerFiles");
            Dictionary<string, List<double>> curveDict = GetGrowthCurves(Path.Combine(weaponPath, "expCurve.json"));


            JArray weapons = new JArray();
            Console.WriteLine("Parsing Bow");
            AddWeapons(weapons, Path.Combine(weaponPath, "Bow"), translationPath, curveDict);
            Console.WriteLine("Parsing Catalyst");
            AddWeapons(weapons, Path.Combine(weaponPath, "Catalyst"), translationPath, curveDict);
            Console.WriteLine("Parsing Claymore");
            AddWeapons(weapons, Path.Combine(weaponPath, "Claymore"), translationPath, curveDict);
            Console.WriteLine("Parsing Polearm");
            AddWeapons(weapons, Path.Combine(weaponPath, "Polearm"), translationPath, curveDict);
            Console.WriteLine("Parsing Sword");
            AddWeapons(weapons, Path.Combine(weaponPath, "Sword"), translationPath, curveDict);

            Console.WriteLine("Weapon parsing done");

            UpdateArtifactInfo(scannerFilesPath, weapons);
        }

        static void GetRepoPaths(out string genshinOptimizerPath, out string adeptiScannerPath)
        {
            string executionPath = Assembly.GetExecutingAssembly().Location;

            var resourcePathsFile = Path.Combine(Path.GetDirectoryName(executionPath)!, @"ResourcePaths.json");

            if (!File.Exists(resourcePathsFile))
            {
                Console.WriteLine("Missing ResourcePaths.json next to executable. Did you forget to copy and rename the example?");
                Environment.Exit(1);
            }

            JObject? resourcePaths = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(resourcePathsFile));

            if (resourcePaths is null)
            {
                Console.WriteLine("ResourcePaths.json deserialize failure");
                Environment.Exit(1);
            }



            genshinOptimizerPath = resourcePaths.GetValue("Genshin Optimizer")?.ToObject<string>() ?? throw new JsonException("ResourcePaths.json was missing Genshin Optimizer path");
            adeptiScannerPath = resourcePaths.GetValue("AdeptiScanner")?.ToObject<string>() ?? throw new JsonException("ResourcePaths.json was missing AdeptiScanner path"); ;
        
            Console.WriteLine("Located repo paths" + Environment.NewLine
                            + "Genshin Optimizer: " + genshinOptimizerPath + Environment.NewLine
                            + "AdeptiScanner: " + adeptiScannerPath);
        }


        static void UpdateArtifactInfo(string scannerFilesPath, JArray weapons)
        {

            string artifactInfoFilePath = Path.Combine(scannerFilesPath, "ArtifactInfo.json");
            string artifactInfoReadableFilePath = Path.Combine(scannerFilesPath, "ArtifactInfo_readable.json");

            string artifactInfoFileBackupPath = artifactInfoFilePath + ".bak";
            string artifactInfoReadableFileBackupPath = artifactInfoReadableFilePath + ".bak";

            if (File.Exists(artifactInfoFileBackupPath)
                || File.Exists(artifactInfoReadableFileBackupPath))
            {
                Console.WriteLine("Did not save to ArtifactInfo: Backup already exists");
                Environment.Exit(1);
            }

            if (!File.Exists(artifactInfoReadableFilePath))
            {
                Console.WriteLine("Did not save to ArtifactInfo: readable version doesn't exist");
                Environment.Exit(1);
            }

            File.Copy(artifactInfoReadableFilePath, artifactInfoReadableFileBackupPath);

            if (File.Exists(artifactInfoFilePath))
            {
                File.Copy(artifactInfoFilePath, artifactInfoFileBackupPath);
            }

            JObject? artifactInfo = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(artifactInfoReadableFilePath));
            if (artifactInfo is null)
            {
                Console.WriteLine("Did not save to ArtifactInfo: Failed to parse");
                Environment.Exit(1);
            }

            artifactInfo.Remove("Weapons");
            artifactInfo.Add("Weapons", weapons);

            File.WriteAllText(artifactInfoReadableFilePath, artifactInfo.ToString(Formatting.Indented));
            File.WriteAllText(artifactInfoFilePath, artifactInfo.ToString(Formatting.None));


            Console.WriteLine("Successfully updated ArtifactInfo");
            File.Delete(artifactInfoFileBackupPath);
            File.Delete(artifactInfoReadableFileBackupPath);
            Console.WriteLine("Deleted backup of ArtifactInfo");

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

        static void AddWeapons(JArray weapons, string categoryPath, string namePath, Dictionary<string, List<double>> curveDict)
        {
            string[] items = Directory.GetDirectories(categoryPath);
            foreach (string itemPath in items)
            {
                string key = itemPath.Split(Path.DirectorySeparatorChar).Last();
                if (key.Equals("QuantumCatalyst", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue; // GO april fools weapon
                }
                string name = key;
                string nameFilePath = Path.Combine(namePath, "weapon_"+key+"_gen.json");
                try
                {
                    var json = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(nameFilePath));
                    if (json != null)
                    {
                        name = json.GetValue("name").ToObject<string>();
                    }
                    else
                    {
                        throw new InvalidOperationException("Weapon locale JSON " + key + " returned null");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get Weapon locale JSON " + nameFilePath + Environment.NewLine + e.Message);
                    Environment.Exit(1);
                }

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
                    {"name", name },
                    { "key", key },
                    { "rarity", rarity },
                    { "stats", statLevelCombo }
                };
                weapons.Add(wep);
            }
        }
    }
}