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

        public InventoryItem()
        {

        }
    }
}
