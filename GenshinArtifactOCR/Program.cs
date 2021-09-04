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

        /// <summary>
        /// Generate all possible text to look for and assign to filter word lists
        /// </summary>
        static void GenerateFilters()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB", false);
            //Main stat filter
            JObject MainJSON = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Form1.appDir + @"\filterdata\MainStats.json"));
            foreach (var stat in MainJSON)
            {
                foreach (var statVal in stat.Value.ToObject<JObject>())
                {
                    double value = statVal.Value.ToObject<double>();
                    string text = stat.Key +  (value).ToString("N0", culture);
                    if (stat.Key.Contains("%"))
                    {
                        text = stat.Key + value.ToString("N1", culture);
                        text = text.Replace("%", "") + "%";
                    }
                    Form1.MainStats.Add(text);
                    //Console.WriteLine(text);
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
                List<int> baserolls = new List<int>();
                List<int> rolls = new List<int>();
                foreach (var value in stat.Value.ToObject<JObject>())
                {
                    baserolls.Add( (int)(value.Value.ToObject<double>() * 100));
                    rolls.Add( (int)(value.Value.ToObject<double>() * 100));
                }
                int start = 0;
                int stop = rolls.Count;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = start; j < stop; j++)
                    {
                        foreach(int value in baserolls)
                        {
                            int tmp = rolls[j] + value;
                            if (!rolls.Contains(tmp))
                                rolls.Add(tmp);
                            else
                                continue;
                        }
                    }
                    start = stop;
                    stop = rolls.Count;
                }
                foreach (int value_int in rolls)
                {
                    double value = value_int / 100.0;
                    string text = stat.Key + "+" + (value).ToString("N0", culture);
                    if (stat.Key.Contains("%"))
                    {
                        text = stat.Key + "+" + value.ToString("N1", culture);
                        text = text.Replace("%", "") + "%";
                    }
                    Form1.Substats.Add(text);
                    //Console.WriteLine(text);
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
                    //Console.WriteLine(text);
                }
            }
        }
    }
}
