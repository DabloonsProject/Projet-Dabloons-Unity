using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string cheminFichier = Path.Combine(Application.streamingAssetsPath, "fichier.json");
        string contenuFichier = File.ReadAllText(cheminFichier);

        classe = JsonUtility.FromJson<Classe>(contenuFichier);

        // Utilisation des données lues du fichier JSON
        Debug.Log($"Nom de la classe : {classe.Nom}");

        foreach (Personnage personnage in classe.Personnages)
        {
            Debug.Log($"Nom du personnage : {personnage.Nom}");
            Debug.Log($"Points de vie : {personnage.PointsDeVie}");
            Debug.Log($"Armure : {personnage.Armure}");

            foreach (string competence in personnage.Competences)
            {
                Debug.Log($"Compétence : {competence}");
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SauvegarderDonneesModifiees()
    {
        string cheminFichier = Path.Combine(Application.streamingAssetsPath, "fichier.json");
        string json = JsonUtility.ToJson(classe, true);
        File.WriteAllText(cheminFichier, json);
    }
}


    [Serializable]
    public class Player
    {
        public string playerId;
        public string playerLoc;
        public string playerNick;
    }
}
