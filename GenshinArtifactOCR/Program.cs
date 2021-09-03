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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory(Form1.appDir);
            Directory.CreateDirectory(Form1.appDir + @"\tessdata");
            Directory.CreateDirectory(Form1.appDir + @"\images");
            Directory.CreateDirectory(Form1.appDir + @"\filterdata");
            GenerateFilters();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void GenerateFilters()
        {
            //Main stat filter
            JObject MainJSON = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Form1.appDir + @"\filterdata\MainStats.json"));
            foreach (var stat in MainJSON)
            {
                foreach (var statVal in stat.Value.ToObject<JObject>())
                {
                    double value = statVal.Value.ToObject<double>();
                    string text = stat.Key +  (value).ToString("N0");
                    if (stat.Key.Contains("%"))
                    {
                        text = stat.Key + value.ToString("N1");
                        text = text.Replace("%", "") + "%";
                    }
                    Form1.MainStats.Add(text);
                    Console.WriteLine(text);
                }
            }


            //Level filter
            for (int i = 0; i < 21; i++)
            {
                Form1.Levels.Add("+" + i);
            }

            //Substat filter
            JObject SubJSON = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Form1.appDir + @"\filterdata\SubStats.json"));
            foreach (var stat in SubJSON)
            {
                List<double> baserolls = new List<double>();
                List<double> rolls = new List<double>();
                foreach (var value in stat.Value.ToObject<JObject>())
                {
                    baserolls.Add(value.Value.ToObject<double>());
                    rolls.Add(value.Value.ToObject<double>());
                }
                for (int i = 0; i < 4; i++)
                {
                    int stop = rolls.Count;
                    for (int j = 0; j < Math.Min(stop, rolls.Count); j++)
                    {
                        foreach(double value in baserolls)
                        {
                            double tmp = rolls[j] + value;
                            if (!rolls.Contains(tmp))
                                rolls.Add(tmp);
                        }
                    }
                }
                foreach (double value in rolls)
                {
                    if (value > 74 && value < 76 && stat.Key.Contains("Elemental"))
                    {
                        int tmp = 0;
                    }
                    string text = stat.Key + "+" + (value).ToString("N0");
                    if (stat.Key.Contains("%"))
                    {
                        text = stat.Key + "+" + value.ToString("N1");
                        text = text.Replace("%", "") + "%";
                    }
                    Form1.Substats.Add(text);
                    Console.WriteLine(text);
                }

            }

            //Set filter
            JObject SetJSON = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Form1.appDir + @"\filterdata\Sets.json"));
            foreach (var set in SetJSON)
            {
                for (int i = 0; i < 6; i++)
                {
                    string text = set.Key + ":(" + i + ")";
                    Form1.Sets.Add(text);
                    Console.WriteLine(text);
                }
            }
        }
    }
}
