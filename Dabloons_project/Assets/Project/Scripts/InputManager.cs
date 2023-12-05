using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*Singleton Class*/
    public static InputManager instance = null; 
    private void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one InputManager!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("InputManager Created.");

        //Input's Initialisation
        playerUsingKeys[0] = false;
        playerController[0] = 0;
        playerAxis[0] = new AxisMapping();
        playerButtons[0] = new ButtonMapping();
        playerState[0] = new InputState();
        oldJoystick = Input.GetJoystickNames();
        //Initialisation
        StartCoroutine(CheckControllers());
    }

    /*Controller gestion*/
    public string[] oldJoystick = null;

    private bool PlayerIsUsingController(int i)
    {
        if (playerController[0] == i)
        {
            return true;
        }
        if (GameManager.instance.twoPlayer && playerController[1] == i)
        {
            return true;
        }
        return false;
    }
    IEnumerator CheckControllers()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            string[] currentJoysticks = Input.GetJoystickNames();
            for (int j = 0; j < currentJoysticks.Length; j++)
            {
                if (j < oldJoystick.Length)
                {
                    if (currentJoysticks[j] != oldJoystick[j])
                    {
                        if (string.IsNullOrEmpty(currentJoysticks[j])) //disconnect
                        {
                            Debug.Log("Controller "+j+" has been disconnected!");
                            if (PlayerIsUsingController(j))
                            {
                                ControllerMenu.instance.whichPlayer = j;
                                ControllerMenu.instance.playerText.text = "Player "+(j+1)+" controller is disconnected !";
                                ControllerMenu.instance.TurnOn(null);
                                //game manager pause gameplay
                            }
                        } // new controller connected
                        Debug.Log("Controller "+j+" is connected using "+currentJoysticks[j]);
                    }
                }
                else
                {
                    Debug.Log("New Controller Connected!");
                }
            }
        }
    }

    /*Fonctionality for Input Managing*/
    public const float deadZone = 0.1f; // value by which we don't detect the controller move in case it's a bit too reactive/défaillant
    private System.Array allKeyCodes = System.Enum.GetValues(typeof(KeyCode));
    public InputState[]     playerState = new InputState[2];
    public ButtonMapping[]  playerButtons = new ButtonMapping[2];
    public AxisMapping[]    playerAxis = new AxisMapping[2];

    public int[]    playerController = new int[2];    
    public bool[]   playerUsingKeys = new bool[2];

    private string[,] playerButtonNames = { {"J1_B1","J1_B2","J1_B3","J1_B4","J1_B5","J1_B6","J1_B7","J1_B8"}};
    private string[,] playerAxisNames = {{"J1_Horizontal", "J1_Vertical"}};  

    /*Remapable Buttons Control means we need to get via the playerIndex the Axis Raw method result*/
    void UpdatePlayerState(int playerIndex)
    {
        if (playerController[playerIndex] < 0) return;
        /*Axis Controls*/
        //Horizontal
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].horinzontal]) < deadZone)
        {
            playerState[playerIndex].left = true;
        }
        playerState[playerIndex].left = false;
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].horinzontal]) > deadZone)
        {
            playerState[playerIndex].right = true;
        }
        //Vertical
        playerState[playerIndex].right = false;
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].vertical]) < deadZone)
        {
            playerState[playerIndex].down = true;
        }
        playerState[playerIndex].down = false;
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].vertical]) > deadZone)
        {
            playerState[playerIndex].up = true;
        }
        playerState[playerIndex].up = false;

        /*Button Controls*/
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].shoot]))
        {
            playerState[playerIndex].shoot = true;
        }
        playerState[playerIndex].shoot = false;
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].options]))
        {
            playerState[playerIndex].options = true;
        }
        playerState[playerIndex].options = false;
        //extras
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].extra1]))
        {
            playerState[playerIndex].extra1 = true;
        }
        playerState[playerIndex].extra1 = false;
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].extra2]))
        {
            playerState[playerIndex].extra2 = true;
        }
        playerState[playerIndex].extra2 = false;
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].extra3]))
        {
            playerState[playerIndex].extra3 = true;
        }
        playerState[playerIndex].extra3 = false;
    }
    private void FixedUpdate() // not compensated by frame rates not rendering frame rates (fixed timestep is set in project setting>Time here it's 0.01666667(1/60, 60 = frame rate wanted))
    {
        UpdatePlayerState(0);
        if (GameManager.instance != null && GameManager.instance.twoPlayer)
        {
            UpdatePlayerState(1);            
        }
    }
    /// <summary>
    /// Checks for inputs and Return the Controller Index
    /// </summary>
    public int DetectControllerButton()
    {
        int result = -1; // no one pressed
        for (int j = 0; j < 1; j++)
        {
            for (int b = 0; b < 8; b++)
            {
                if( Input.GetButton(playerButtonNames[j,b])) return j;
            }
        }
        return result;
    }
    public int DetectKeyPress()
    {
        foreach (KeyCode key in allKeyCodes)
        {
            if (Input.GetKey(key)) return ((int)key);
        }
        return -1; // no one pressed
    }
    /*Checks for inputs and Set the player Controller or Keyboard options*/
    public bool CheckForPlayerInput(int playerIndex)
    {
        int controller = DetectControllerButton();
        int key = DetectKeyPress();
        if (controller > -1)
        {
            playerController[playerIndex] = controller;
            playerUsingKeys[playerIndex] = false;
            Debug.Log("Player "+playerIndex+" is set to controller"+ controller);
            return true;
        }
        if (key > -1)
        {
            playerUsingKeys[playerIndex] = true;
            playerController[playerIndex] = -1;
            Debug.Log("Player "+playerIndex+" is set to keyboard");
            return true;
        }
        return false;
    }
}
public class InputState
{
    public bool left, right, up, down;
    public bool shoot, options, extra1, extra2, extra3;
}
public class ButtonMapping
{
    public byte shoot = 0;
    public byte options = 2;
    public byte extra1 = 5;
    public byte extra2 = 6;
    public byte extra3 = 7;
}
public class AxisMapping
{
    public byte horinzontal = 0;
    public byte vertical = 1;
}