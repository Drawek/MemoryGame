using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    public string tutorialMessage;
    public float timeBetweenMessage, startDelay;
    private float startTimer = 0, timer = 0;
    private bool isIn, hasPlayed;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (!hasPlayed)
        {
            if (isIn)
                startTimer += Time.deltaTime;
            
            if(startTimer > startDelay)
            {
                hasPlayed = true;
                StartCoroutine(GameManager.Instance.playerUI.PlayTutorial());
            }
        }
        else
        {
            if (isIn)
                timer += Time.deltaTime;

            if (timer > timeBetweenMessage)
            {
                timer = 0;
                StartCoroutine(GameManager.Instance.playerUI.PlayTutorial());
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isIn)
            {
                GameManager.Instance.playerUI.tutorialText = tutorialMessage;
                isIn = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isIn = false;
        timer = 0;
    }
}
