using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public string loadLevelName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.playerUI.loadSceneName = loadLevelName;
            GameManager.Instance.playerUI.animator.SetTrigger("coverScreen");
            GameManager.Instance.playerUI.spawnToObject = "SpawnStart";
        }
    }
}
