using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTeleport : MonoBehaviour
{

    public LevelManager[] levels;

    // Update is called once per frame
    void Update()
    {
        Teleport();
    }

    void Teleport()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Stuff(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Stuff(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Stuff(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Stuff(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Stuff(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Stuff(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Stuff(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Stuff(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Stuff(9);
        }
    }

    void Stuff(int level)
    {
        GameManager.Instance.lastLevel = GameManager.Instance.curLevel;
        NewPlayer.Instance.frozen = true;
        GameManager.Instance.entrance = levels[level-1].checkPoint.transform;
        GameManager.Instance.checkPoint = levels[level - 1].checkPoint;
        GameManager.Instance.curLevel = levels[level - 1];
        GameManager.Instance.playerUI.animator.SetTrigger("fadeScreen");
    }
}
