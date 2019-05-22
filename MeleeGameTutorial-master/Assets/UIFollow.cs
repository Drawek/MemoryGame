using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(NewPlayer.Instance.gameObject.transform.position + Vector3.up * 2.3f);
    }
}
