using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource darkMusic, lightMusic;

    // Update is called once per frame
    void Update()
    {
        if(darkMusic != null && lightMusic != null)
        {
            if (GameManager.Instance.isChaosWorld)
            {
                darkMusic.volume = Mathf.MoveTowards(darkMusic.volume, 1, Time.deltaTime);
                lightMusic.volume = Mathf.MoveTowards(lightMusic.volume, 0, Time.deltaTime);
            }
            else
            {
                darkMusic.volume = Mathf.MoveTowards(darkMusic.volume, 0, Time.deltaTime);
                lightMusic.volume = Mathf.MoveTowards(lightMusic.volume, 1, Time.deltaTime);
            }
        }
    }
}
