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
    public float speed;
    public int money;
}

[System.Serializable]
public class Character : Entity
{
    public int escape;

    /*
    public Dictionary<string, int> experiences;

    public void InitializeExperiences()
    {
        if (experiences == null)
        {
            experiences = new Dictionary<string, int>();
        }
    }
    */
}

[System.Serializable]
public class PNJ : Entity
{
    public int level;
    public int age;
    public string race;
    public List<string> personality; // contains (gender, gender expression, personnality traits, loisirs, addictions...)
    public int liking; // how much does this pnj likes the player


    public Quest[] questsList;
    public Dictionary<string, Quest> quests;

    public void InitializePNJ()
    {
        quests = ConvertArrayToDictionary(questsList);
    }

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

    public Dictionary<string, Quest> ConvertArrayToDictionary(Quest[] questsList)
    {
        Dictionary<string, Quest> dictionary = new Dictionary<string, Quest>();
        foreach (Quest quest in questsList)
        {
            dictionary[quest.name] = quest;
        }
        return dictionary;
    }
}

[System.Serializable]
public class Quest
{
    public string name;
    public int value;
    public string value2;
}
//obviously all entities will have an inventory/ PNJ have quests (dealt with by dialogues ?)

/* Compétences start

public skills passive;
public skills active;

[System.Serializable]
public class skills
{
    public string name;
    public string description;
    public Dictionary<string, int> stats;

    public void skills()
    {
        stats = new Dictionary<string, int>();
    }
 }
/*
"passive":{
                "name": "competence",
                "description": "La compétence sert à faire ça",
                "stats": {
                    "comp_1": 10,
                    "comp_2": -10
                }
            },
            "active":{
                "name": "competence2",
                "description": "La compétence sert à faire ça",
                "stats": {
                    "comp_1": 10,
                    "comp_2": -10
                }
            }
*/

