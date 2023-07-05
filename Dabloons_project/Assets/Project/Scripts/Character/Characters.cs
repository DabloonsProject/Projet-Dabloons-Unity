using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Characters : MonoBehaviour
{
    private PersonnagesData classe;

    void Start()
    {        
        string cheminFichier = Path.Combine(Application.streamingAssetsPath, "Characters_data.json");
        string contenuFichier = File.ReadAllText(cheminFichier);

        classe = JsonUtility.FromJson<PersonnagesData>(contenuFichier);
        Debug.Log($"Nom de la classe : {classe.nom}");

        foreach (Character personnage in classe.personnages)
        {
            Debug.Log($"Nom du personnage : {personnage.nom}");
            Debug.Log($"Points de vie : {personnage.vie}");
            Debug.Log($"Points de vie : {personnage.mana}");
            Debug.Log($"Armure : {personnage.armure}");
            
        }
    }

/*
    void SauvegarderDonneesModifiees()
    {
        string cheminFichier = Path.Combine(Application.streamingAssetsPath, "Characters_data.json");
        string json = JsonUtility.ToJson(classe, true);
        File.WriteAllText(cheminFichier, json);
    }
*/
}

[System.Serializable]
public class PersonnagesData
{
    public string nom;
    public Character[] personnages;
}