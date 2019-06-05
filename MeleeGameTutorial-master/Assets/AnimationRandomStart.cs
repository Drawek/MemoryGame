using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomStart : MonoBehaviour
{
    public string animationName;
    private Animator thisAnim;
    void Start()
    {
        thisAnim.GetComponent<Animator>();
        thisAnim.Play("animationName", 1, Random.Range(0.0f, 5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
