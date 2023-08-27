using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AdeptiScanner_GI
{
    public class Character
    {
        public string key;
        int level;
        int constellation;
        int ascension;
        int auto;
        int skill;
        int burst;
        public static Character FromEnkaData(JObject enkaData)
        {
            Character character = new Character();

            int avatarId = enkaData["avatarId"].ToObject<int>();
            if (Database.CharacterNames.TryGetValue(avatarId, out string charKey))
            {
                character.key = charKey;
            } else
            {
                character.key = "UNKNOWN_" + avatarId;
            }

            JObject propMap = enkaData["propMap"].ToObject<JObject>();
            character.ascension = propMap["1002"].ToObject<JObject>()["ival"].ToObject<int>();
            character.level = propMap["4001"].ToObject<JObject>()["ival"].ToObject<int>();
            character.constellation = 0;
            if (enkaData.ContainsKey("talentIdList"))
            {
                character.constellation = enkaData["talentIdList"].ToObject<JArray>().Count();
            }

            JObject skillMap = enkaData["skillLevelMap"].ToObject<JObject>();
            
            foreach( var test in skillMap)
            {
                if (Database.SkillTypes.ContainsKey(test.Key))
                {
                    string skillType = Database.SkillTypes[test.Key];
                    if (skillType.Equals("auto"))
                    {
                        character.auto = test.Value.ToObject<int>();
                    }
                    else if (skillType.Equals("skill"))
                    {
                        character.skill = test.Value.ToObject<int>();
                    }
                    else if (skillType.Equals("burst"))
                    {
                        character.burst = test.Value.ToObject<int>();
                    }
                }
            }

            return character;
        }
        public JObject toGOODCharacter()
        {
            JObject result = new JObject
            {
                { "key", JToken.FromObject(key) },
                { "level", JToken.FromObject(level) },
                { "constellation", JToken.FromObject(constellation) },
                { "ascension", JToken.FromObject(ascension) }
            };
            JObject talents = new JObject
            {
                { "auto", JToken.FromObject(auto) },
                { "skill", JToken.FromObject(skill) },
                { "burst", JToken.FromObject(burst) }
            };
            result.Add("talent", talents);

            return result;
        }


        public static JObject listToGOODCharacter(List<Character> items)
        {
            JObject result = new JObject();
            JArray charJArr = new JArray();
            foreach (Character item in items)
            {
                charJArr.Add(item.toGOODCharacter());
            }
            result.Add("characters", charJArr);
            return result;
        }
    }
}
