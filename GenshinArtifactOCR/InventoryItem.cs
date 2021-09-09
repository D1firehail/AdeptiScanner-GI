using System;
using System.Collections.Generic;

namespace GenshinArtifactOCR
{
    class InventoryItem
    {
        public Tuple<string, string> piece;
        public Tuple<string, string, double> main;
        public Tuple<string, int> level;
        public List<Tuple<string, string, double>> subs;
        public Tuple<string, string> set;
        public Tuple<string, string> character;
        public bool locked = false;
        public int rarity = 0;

        public InventoryItem()
        {

        }

        public override string ToString()
        {
            string text = "";

            text += "Piece: ";
            if (piece != null)
                text += piece + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Main: ";
            if (main != null)
                text += main + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Rarity: " + rarity + Environment.NewLine;

            text += "Level: ";
            if (level != null)
                text += level + Environment.NewLine;
            else
                text += "Null-------" + Environment.NewLine;

            text += "Subs: ";
            if (subs != null)
            {
                text += subs.Count + Environment.NewLine;
                foreach (var sub in subs)
                {
                    text += sub + Environment.NewLine;
                }
            }
            else
                text += "Null-------" + Environment.NewLine;

            text += "Set: ";
            if (set != null)
                text += set + Environment.NewLine;
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
    }
}
