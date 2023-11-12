using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*Singleton Class*/
    public static  GameManager instance = null;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than 1 GameManager!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("GameManager Created.");
    }

    /*Multiple players*/
    public bool twoPlayer = false;
}
