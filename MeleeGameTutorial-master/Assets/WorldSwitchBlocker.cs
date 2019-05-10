using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitchBlocker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NewPlayer.Instance.canSwitchWorld = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NewPlayer.Instance.canSwitchWorld = true;
        }
    }
}
