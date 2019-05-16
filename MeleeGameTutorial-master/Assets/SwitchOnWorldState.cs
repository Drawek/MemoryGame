using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnWorldState : MonoBehaviour
{
    public GameObject chaosObject, orderObject;
    public bool isCollected;

    // Update is called once per frame
    void Update()
    {
        if (!isCollected) {
            if (GameManager.Instance.isChaosWorld)
            {
                chaosObject.SetActive(true);
                orderObject.SetActive(false);
            }
            else
            {
                chaosObject.SetActive(false);
                orderObject.SetActive(true);
            }
        }
        else
        {
            chaosObject.SetActive(false);
            orderObject.SetActive(false);
            if (GameManager.Instance.resetWorlds)
            {
                isCollected = false;
            }
        }
    }
}
