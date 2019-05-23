using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMemoryMovie : MonoBehaviour
{
    public void Fade()
    {
        GameManager.Instance.playerUI.fadeType = PlayerUI.FadeType.loadLevel;
        GameManager.Instance.playerUI.loadSceneName = "EndingMovie";
        GameManager.Instance.playerUI.animator.SetTrigger("fadeScreen");
    }
}
