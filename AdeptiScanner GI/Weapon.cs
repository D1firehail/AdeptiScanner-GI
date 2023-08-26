using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AdeptiScanner_GI
{
    public class Weapon
    {
        public Tuple<string, string, int> name;
        public Tuple<string, int, int> level;
        public int? refinement;
        public Tuple<string, string> character;
        public bool locked = false;

        public Weapon()
        {

        }

        public override string ToString()
        {
            string text = "";

            text += "Name: ";
            if (name != null)
                text += name + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Level: ";
            if (level != null)
                text += level + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Refinement: ";
            if (refinement != null)
                text += refinement + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Char: ";
            if (character != null)
                text += character + Environment.NewLine;
            else
                text += "Null" + Environment.NewLine;

            text += "Locked: " + locked + Environment.NewLine;

            return text;
        }

        public JObject toGOODWeapon()
        {
            JObject result = new JObject();

            if (name != null)
                result.Add("key", JToken.FromObject(name.Item2));
            if (level != null)
            {
                result.Add("level", JToken.FromObject(level.Item2));
                result.Add("ascension", JToken.FromObject(level.Item3));
            }
            if (refinement != null)
                result.Add("refinement", JToken.FromObject(refinement.Value));
            else if (name != null && name.Item3 < 3)
                result.Add("refinement", 1);
            if (character != null)
                result.Add("location", JToken.FromObject(character.Item2));
            else
                result.Add("location", JToken.FromObject(""));

            result.Add("lock", JToken.FromObject(locked));
            return result;
        }

        public static JObject listToGOODWeapons(List<Weapon> items)
        {
            JObject result = new JObject();
            JArray weaponJArr = new JArray();
            foreach (Weapon item in items)
            {
                weaponJArr.Add(item.toGOODWeapon());
            }
            result.Add("weapons", weaponJArr);
            return result;
        }
    }
}
