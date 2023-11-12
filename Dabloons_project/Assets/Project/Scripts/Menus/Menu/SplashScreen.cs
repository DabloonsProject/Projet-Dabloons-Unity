using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            VideoFinished();
        }
    }
    /*When timer runs out we load the Main Scene with a MainMenu UI*/
    void VideoFinished()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
