using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource btn_Sound, music;
    private bool pressedPlay;

    private void Update()
    {
        if (pressedPlay)
            music.volume = Mathf.MoveTowards(music.volume, 0, Time.deltaTime);
    }

    public void PlayGame()
    {
        if (!pressedPlay)
        {
            pressedPlay = true;
            if (btn_Sound != null)
                btn_Sound.Play();
            GetComponent<Animator>().SetTrigger("Fade");
        }
    }

    public void ExitGame()
    {
        if (!pressedPlay)
        {
            if (btn_Sound != null)
                btn_Sound.Play();
            Application.Quit();
        }
    }
    public void FadeScreen()
    {
        SceneManager.LoadScene("IntroMovie");
    }
}
