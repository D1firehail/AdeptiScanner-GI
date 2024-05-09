using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdeptiScanner_GI
{
    public partial class EnkaTab : UserControl
    {
        Timer enkaTimer;

        public EnkaTab()
        {
            enkaTimer = new Timer();
            enkaTimer.Interval = 500; //ms
            enkaTimer.Tick += EnkaTimerOnTick;
            InitializeComponent();
            UpdateCooldown();
            enkaTimer.Enabled = true;
        }

        private void EnkaTimerOnTick(object sender, EventArgs e)
        {
            UpdateCooldown();
        }

        private void UpdateCooldown()
        {
            TimeSpan remainingTime = EnkaApi.GetRemainingCooldown();
            // round up, saying cooldown is over too early would be unhelpful
            var displayTime = (int)Math.Ceiling(remainingTime.TotalSeconds);
            string message = "Cooldown: " + displayTime.ToString("D2") + "s";
            label_cooldown.Text = message;
            // colour indication for convenience
            label_cooldown.BackColor = displayTime switch
            {
                > 10 => Color.IndianRed,
                > 0 => Color.Orange,
                _ => Color.Transparent
            };
        }

        private void btn_Fetch_Click(object sender, EventArgs e)
        {
            string uid = new string(text_UID.Text);
            EnkaApi.RequestUid(uid);
        }

        public void UpdateMissingChars(List<Artifact> artifacts, List<Weapon> weapons, List<Character> characters) 
        {
            List<Tuple<string, string>> names = new List<Tuple<string, string>>();
            foreach (Artifact arti in artifacts)
            {
                if (arti.character != null && !names.Any(name => name.Item2.Equals(arti.character.Item2)))
                {
                    //if name is non-null and not already added, add it!
                    names.Add(arti.character);
                }
            }

            foreach (Weapon wep in weapons) 
            { 

                if (wep.character != null && !names.Any(name => name.Item2.Equals(wep.character.Item2)))
                {
                    //if name is non-null and not already added, add it!
                    names.Add(wep.character);
                }
            }

            //filter out names that are already fetched
            List<Tuple<string, string>> unfetchedNames = names.Where(name => !characters.Any(c => c.key.Equals(name.Item2))).ToList();

            string missingChars = string.Empty;
            foreach (Tuple<string, string> name in unfetchedNames)
            {
                missingChars += name.Item1.Replace("Equipped: ", "") + Environment.NewLine;
            }

            text_remainingCharacters.Text = missingChars;
        }
    }
}
