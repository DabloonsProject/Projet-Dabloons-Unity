using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Entity_Manager : MonoBehaviour
{
    private EntitiesData all_entities;

    void Start()
    {        
        string filePath = Path.Combine(Application.streamingAssetsPath, "Entities_data.json");
        string filecontent = File.ReadAllText(filePath);
        all_entities = JsonUtility.FromJson<EntitiesData>(filecontent);

        Debug.Log($"Start");
        debugValues();

        foreach (Character personnage in all_entities.personnages)
        {
            personnage.life += 10;
            personnage.speed -= 1;
        }

        Debug.Log($"Mods zetger tgertg rth rthrt hrj");
        SaveModifs();
        string filePaths = Path.Combine(Application.dataPath, "Resources/Player.json");
        string file_contents = File.ReadAllText(filePaths);
        all_entities = JsonUtility.FromJson<EntitiesData>(file_contents); 
        /* For one character
        string filePaths = Path.Combine(Application.dataPath, "Resources/Player.json");
        string file_contents = File.ReadAllText(filePaths);
        all_entities.personnages[0] = JsonUtility.FromJson<Character>(file_contents)*/
        debugValues();

    }
/*

getting competences and modifying

foreach (KeyValuePair<string, int> modifier in ability.modifiers)
{
    string modifierName = modifier.Key;
    int modifierValue = modifier.Value;
    
    // Use the modifierName and modifierValue to modify the corresponding value in your fight manager
    fightManager.ModifyValue(modifierName, modifierValue);
}
*/
    void debugValues()
    {
        foreach (Character personnage in all_entities.personnages)
        {
            Debug.Log($"Nom du personnage : {personnage.name}");
            Debug.Log($"Points de vie : {personnage.life}");
            Debug.Log($"Points de mana : {personnage.mana}");
            Debug.Log($"Armure : {personnage.armour}");
            Debug.Log($"Vitesse: {personnage.speed}");
            Debug.Log($"Fuite: {personnage.escape}");
        }
        foreach (PNJ pnj in all_entities.pnjs)
        { 
            pnj.InitializePNJ(); 
            Debug.Log($"Nom du personnage : {pnj.name}");
            Debug.Log($"Points de vie : {pnj.life}");
            Debug.Log($"Points de mana : {pnj.mana}");
            Debug.Log($"Armure : {pnj.armour}");
            Debug.Log($"Vitesse: {pnj.speed}");
            Debug.Log($"Age : {pnj.age}");
            Debug.Log($"Race: {pnj.race}");
            
            foreach (KeyValuePair<string,Quest> questEntry in pnj.quests)
            {                
                string questName = questEntry.Key;
                Quest quest = questEntry.Value;
                int value = quest.value;
                string value2 = quest.value2;

                Debug.Log($"Quête : {questName}, Valeur : {value}, Valeur2 : {value2}");
            }
        }
    }
    void SaveModifs()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/Player.json");
        if (File.Exists(filePath))
        {
            string json = JsonUtility.ToJson(all_entities, true);
            /* for one character
            int characterIndex = 0;
            Character character = all_entities.personnages[characterIndex];
            string json = JsonUtility.ToJson(character, true);*/
            File.WriteAllText(filePath, json);
        }
        else
        {
            Debug.LogError("Failed to load JSON file from Resources folder: " + filePath);
        }
    }
}

[System.Serializable]
public class EntitiesData
{
    public Character[] personnages;
    public PNJ[] pnjs;
}

/*string derivedTypeName = GetType().ToString();
Debug.Log($"Nom de derivedTypeName: {derivedTypeName}"); (get type)*/