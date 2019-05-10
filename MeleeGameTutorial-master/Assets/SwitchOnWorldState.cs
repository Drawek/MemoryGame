using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnWorldState : MonoBehaviour
{
    public GameObject chaosObject, orderObject;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isChaosWorld)
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
}
