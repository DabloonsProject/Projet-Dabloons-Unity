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

        /*
        Debug.Log($"Start");
        debugValues();

        foreach (Character personnage in all_entities.personnages)
        {
            personnage.vie += 10;
            personnage.vitesse -= 1;
        }
        foreach (PNJ pnjs in all_entities.pnjs)
        {
            pnjs.attribute_1 += "_seen";
        }

        Debug.Log($"Mods");
        SaveModifs();
        string filePaths = Path.Combine(Application.dataPath, "Resources/Player.json");
        string file_contents = File.ReadAllText(filePaths);
        all_entities = JsonUtility.FromJson<EntitiesData>(file_contents);
        debugValues();
        */
    }
/*
    void debugValues()
    {
        foreach (Character personnage in all_entities.personnages)
        {
            Debug.Log($"Nom du personnage : {personnage.nom}");
            Debug.Log($"Points de vie : {personnage.vie}");
            Debug.Log($"Points de mana : {personnage.mana}");
            Debug.Log($"Armure : {personnage.armure}");
            Debug.Log($"Vitesse: {personnage.vitesse}");
        }
        foreach (PNJ pnjs in all_entities.pnjs)
        {
            Debug.Log($"Nom du personnage : {pnjs.nom}");
            Debug.Log($"Points de vie : {pnjs.vie}");
            Debug.Log($"Points de mana : {pnjs.mana}");
            Debug.Log($"Armure : {pnjs.armure}");
            Debug.Log($"Attribut 1 : {pnjs.attribute_1}");
            Debug.Log($"Attribut 2 : {pnjs.attribute_2}");
        }
    }
*/
    void SaveModifs()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/Player.json");
        if (File.Exists(filePath))
        {
            string json = JsonUtility.ToJson(all_entities, true);
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