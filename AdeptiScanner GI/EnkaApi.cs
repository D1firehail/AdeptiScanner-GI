using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;

namespace AdeptiScanner_GI
{
    internal static class EnkaApi
    {

        static List<Character> ProcessData()
        {
            //TODO Enka API interaction
            var text = Clipboard.GetText();

            List<Character> characters = new List<Character>();
            var enkaJson = JsonConvert.DeserializeObject<JObject>(text);
            List<JObject> chars = enkaJson["avatarInfoList"].ToObject<List<JObject>>();
            foreach (JObject charJson in chars)
            {
                characters.Add(Character.FromEnkaData(charJson));
            }
            return characters;
        }
    }
}
