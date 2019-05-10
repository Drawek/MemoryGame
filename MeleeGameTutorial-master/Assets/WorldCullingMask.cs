using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCullingMask : MonoBehaviour
{
    private Camera cam;
    public LayerMask chaosWorld, orderWorld;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isChaosWorld)
            cam.cullingMask = chaosWorld;
        else
            cam.cullingMask = orderWorld;
    }
}
