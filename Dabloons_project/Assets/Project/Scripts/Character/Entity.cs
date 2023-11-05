using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Entity
{
    public string name;
    public int life;
    public int mana;
    public int armour;
    public int money;
    public float speed;
}

[System.Serializable]
public class Character : Entity
{
    public List<string> skillsList;
}

[System.Serializable]
public class Player : Character
{
    public int escape;
    public Dictionary<string, int> experiences;
    public void InitializeExperiences()
    {
        if (experiences == null)
        {
            experiences = new Dictionary<string, int>();
            experiences["xp"] = 0;
            experiences["xpWeapon1"] = 0;
            experiences["xpWeapon2"] = 0;
            experiences["xpWeapon3"] = 0;
        }
    }
    
    /*inventory*/
}

[System.Serializable]
public class PNJ : Entity
{
    public int level;
    public int age;
    public string race;
    public List<string> personality; // contains (gender, gender expression, personnality traits, loisirs, addictions...)
    public int liking; // how much does this pnj likes the player

    /*inventory*/
    public Quest[] questsList;
    public Dictionary<string, Quest> quests;

    public void QuestsInit()
    {
        quests = ConvertArrayToDictionary(questsList);
    }

    public Dictionary<string, Quest> ConvertArrayToDictionary(Quest[] questsList)
    {
        Dictionary<string, Quest> dictionary = new Dictionary<string, Quest>();
        foreach (Quest quest in questsList)
        {
            dictionary[quest.name] = quest;
        }
        return dictionary;
    }

    /*functions*/
    public int GetQuestValueInt(string questName, string valueName)
    {
        if (quests.TryGetValue(questName, out Quest quest))
        {
            var propertyInfo = typeof(Quest).GetProperty(valueName);
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(int))
            {
                object value = propertyInfo.GetValue(quest);
                if (value != null)
                {
                    return (int)value;
                }
            }
        }
        return 0;
    }

    public string GetQuestValueString(string questName, string valueName)
    {
        if (quests.TryGetValue(questName, out Quest quest))
        {
            var propertyInfo = typeof(Quest).GetProperty(valueName);
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(string))
            {
                object value = propertyInfo.GetValue(quest);
                if (value != null)
                {
                    return (string)value;
                }
            }
        }
        return string.Empty;
    }
}
