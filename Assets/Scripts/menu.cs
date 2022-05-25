using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    private void Start()
    {
        PlatformController.singleton.Init("COM3", 115200);
    }
    public void playGame()
    {
        SceneManager.LoadScene(1);
    } 
    
    public void quitGame()
    {
        Application.Quit();
    }
}
