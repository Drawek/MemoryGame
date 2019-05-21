using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    public LevelManager goToLevel;
    public Transform entrance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.lastLevel = GameManager.Instance.curLevel;
            NewPlayer.Instance.frozen = true;
            GameManager.Instance.entrance = entrance;
            GameManager.Instance.checkPoint = entrance.gameObject;

            GameManager.Instance.curLevel = goToLevel;
            GameManager.Instance.playerUI.animator.SetTrigger("fadeScreen");          
        }
    }
}
