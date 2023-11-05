using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class skills 
    {
    public skill passive;
    public skill active;
}

[System.Serializable]
public class skill
{
    public string name;
    public string description;
    public int time; /*number of turns the skill is effective*/
    public List<Stat> statsList; 
    public Dictionary<string, int> stats; 

    public void InitSkill()
    {
        stats = ConvertListToDictionary(statsList);
    }

    public Dictionary<string, int> ConvertListToDictionary(List<Stat> statsList)
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        foreach (Stat stat in statsList)
        {
            dictionary[stat.name] = stat.value;
        }
        return dictionary;
    }
}

[System.Serializable]
public class Stat 
{
    public string name; // Represents a value modified by the skill
    public int value; // Represents the value per turn it'll be modified by or the value on activation
}