using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public GameObject dream;
    public AudioSource sound;
    private bool hasBeenPickedUp;
    public int dir = 1;
    public int toleranceIncrease;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!hasBeenPickedUp)
            {
                NewPlayer.Instance.stressTolerance += toleranceIncrease;
                hasBeenPickedUp = true;
                GameManager.Instance.everything.transform.localScale = new Vector3(dir, 1, 1);
            }
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
