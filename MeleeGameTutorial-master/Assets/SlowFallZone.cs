using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFallZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NewPlayer.Instance.slowfallModifier = 0.25f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NewPlayer.Instance.slowfallModifier = 1f;
        }
    }
}
