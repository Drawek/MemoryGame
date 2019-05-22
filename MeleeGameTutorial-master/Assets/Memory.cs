using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public GameObject dream;
    public AudioSource sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (sound != null)
                sound.Play();
            dream.SetActive(true);
            GetComponent<Animator>().SetTrigger("Dissapear");
        }
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
