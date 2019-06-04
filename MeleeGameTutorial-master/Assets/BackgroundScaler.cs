using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public float cameraDistance;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = transform.localScale.x * (transform.position.z + cameraDistance) * Vector3.one /cameraDistance;
    }
}
