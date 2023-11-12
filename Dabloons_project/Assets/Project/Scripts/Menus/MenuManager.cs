using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    /*Singleton Class*/
    public static MenuManager instance = null; 
    private void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one MenuManager!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("MenuManager Created.");
    }

    /*Fonctionality for UI MenuManaging*/
    internal Menu activeMenu = null;
    public void SwitchToGameplayMenu()
    {
    }
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.LoadScene("TitleScreenMenu", LoadSceneMode.Additive);
    }
}
