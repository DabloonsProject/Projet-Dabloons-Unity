using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMenu : Menu
{
    /*Menus Class*/
    public static ControllerMenu instance = null;
    private void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one ControllerMenu!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    /*Controller functionality*/
    public int whichPlayer = 0;
    public Text playerText = null;
    private void Update()
    {
        if(ROOT.gameObject.activeInHierarchy)
        {
            if (InputManager.instance.CheckForPlayerInput(whichPlayer))
            {
                TurnOff(false);
                //Game manager resume gameplay
            }
        }
    }

    /*Buttons*/
    public void OnPressButton()
    {
        TurnOff(true);
    }
}
