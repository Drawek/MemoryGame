using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    
    public void Resume()
    {
        GameManager.Instance.gameIsPaused = false;
    }
    public void Title(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
