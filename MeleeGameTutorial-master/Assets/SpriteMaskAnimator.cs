using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskAnimator : MonoBehaviour
{
    private SpriteMask spriteMask;
    private SpriteRenderer spriteRend;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        spriteMask = GetComponent<SpriteMask>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsDarkWorld", GameManager.Instance.isChaosWorld);
        spriteMask.sprite = spriteRend.sprite;
    }
}
