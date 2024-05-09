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
            List<CharacterNameData> names = new List<CharacterNameData>();
            foreach (Artifact arti in artifacts)
            {
                if (arti.character != null && !names.Any(name => name.Key.Equals(arti.character.Value.Key)))
                {
                    //if name is non-null and not already added, add it!
                    names.Add(arti.character.Value);
                }
            }

            foreach (Weapon wep in weapons) 
            {

                if (wep.character != null && !names.Any(name => name.Key.Equals(wep.character.Value.Key)))
                {
                    //if name is non-null and not already added, add it!
                    names.Add(wep.character.Value);
                }
            }

            //filter out names that are already fetched
            List<CharacterNameData> unfetchedNames = names.Where(name => !characters.Any(c => c.key.Equals(name.Key))).ToList();

            string missingChars = string.Empty;
            foreach (CharacterNameData name in unfetchedNames)
            {
                missingChars += name.Text.Replace("Equipped: ", "") + Environment.NewLine;
            }

            text_remainingCharacters.Text = missingChars;
        }
    }
}
