using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Entity_Manager : MonoBehaviour
{
    private EntitiesData all_entities;
    private Player player;

    void Start()
    {   
        FromJsonEntities();
        SetIndexPlayer(0);
        SaveModifsPlayer();
        SaveModifsPNJs();
    }

    void FromJsonEntities(){
        string filePath = Path.Combine(Application.streamingAssetsPath, "Entities_data.json");
        string filecontent = File.ReadAllText(filePath);
        all_entities = JsonUtility.FromJson<EntitiesData>(filecontent);
        foreach (PNJ pnj in all_entities.pnjs)
        { 
            pnj.QuestsInit();
        }
        all_entities.InitEntitiesData();
        debugLogs();
    }
    void SaveModifsAll()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/Entities.json");
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
    void SaveModifsPlayer()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/Player.json");
        if (File.Exists(filePath))
        {
            string json = JsonUtility.ToJson(player, true);
            File.WriteAllText(filePath, json);
        }
        else
        {
            Debug.LogError("Failed to load JSON file from Resources folder: " + filePath);
        }
    }
    void SaveModifsPNJs()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/PNJs.json");
        if (File.Exists(filePath))
        {
            List<string> jsonPNJs = new List<string>();

            foreach (PNJ pnj in all_entities.pnjs)
            {
                string json = JsonUtility.ToJson(pnj, true);
                jsonPNJs.Add(json);
            }
            string finalJson = "[" + string.Join(",", jsonPNJs.ToArray()) + "]";
            File.WriteAllText(filePath, finalJson);
        }
        else
        {
            Debug.LogError("Failed to load JSON file from Resources folder: " + filePath);
        }
    }
    void SetIndexPlayer(int index){
    int playerIndex = index;

    if (all_entities.personnages[playerIndex] is Character)
    {
        Character characterChosen = (Character)all_entities.personnages[playerIndex];
        
        player = new Player(); // Initialisez un nouvel objet Player

        player.name = characterChosen.name;
        player.life = characterChosen.life;
        player.mana = characterChosen.mana;
        player.armour = characterChosen.armour;
        player.money = characterChosen.money;
        player.speed = characterChosen.speed;
        player.skillsList = characterChosen.skillsList;

        Debug.Log(player.life);
    }
}


    void debugLogs() {
        Debug.Log($"Start !");
        foreach (Character personnage in all_entities.personnages)
        {
            personnage.life += 20;
            personnage.speed -= 1;
        }
        Debug.Log($"Mods Done!");
        SaveModifsAll(); 
        Debug.Log($"Mods Saved !");
    }


/*
    void debugValues()
    {
        Debug.Log($"Personnages");
        foreach (Character personnage in all_entities.personnages)
        {
            Debug.Log($"Nom du personnage : {personnage.name}");
            Debug.Log($"Points de vie : {personnage.life}");
            Debug.Log($"Points de mana : {personnage.mana}");
            Debug.Log($"Armure : {personnage.armour}");
            Debug.Log($"Vitesse: {personnage.speed}");
            Debug.Log($"Money: {personnage.money}");
        }
        Debug.Log($"PNJs");
        foreach (PNJ pnj in all_entities.pnjs)
        { 
            Debug.Log($"Nom du personnage : {pnj.name}");
            Debug.Log($"Points de vie : {pnj.life}");
            Debug.Log($"Points de mana : {pnj.mana}");
            Debug.Log($"Armure : {pnj.armour}");
            Debug.Log($"Vitesse: {pnj.speed}");
            Debug.Log($"Money: {pnj.money}");
            Debug.Log($"Age : {pnj.age}");
            Debug.Log($"Race: {pnj.race}");
            foreach (KeyValuePair<string,Quest> questEntry in pnj.quests)
            {                
                string questName = questEntry.Key;
                Quest quest = questEntry.Value;

                Debug.Log($"Quête : {questName}");
            }
        }
    }*/
}

[System.Serializable]
public class EntitiesData
{
    public Character[] personnages;
    public PNJ[] pnjs;
    public Dictionary<string, PNJ> pnjs_dic;

    public void InitEntitiesData()
    {
        pnjs_dic = ConvertArrayToDictionary(pnjs);
    }

    public Dictionary<string, PNJ> ConvertArrayToDictionary(PNJ[] pnjs)
    {
        Dictionary<string, PNJ> dictionary = new Dictionary<string, PNJ>();
        foreach (PNJ pnj in pnjs)
        {
            dictionary[pnj.name] = pnj;
        }
        return dictionary;
    }
}