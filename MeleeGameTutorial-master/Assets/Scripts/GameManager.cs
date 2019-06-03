using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Singleton instantiation
    public GameObject checkPoint;
    [HideInInspector] public Transform entrance;
	public AudioSource audioSource;
	public DialogueBox dialogueBox;
	public PlayerUI playerUI;

    public bool gameIsPaused;

	public int gemAmount;
	public Dictionary<string, Sprite> inventory  = new Dictionary<string, Sprite>();
    public bool isChaosWorld = true;
    [HideInInspector]
    public bool wasChaosWorld = true, switchingWorlds, resetWorlds;
    public bool playerIsDead;
    public int startChaos;
    public LevelManager curLevel, lastLevel;
    public GameObject everything;
    private static GameManager instance;
	public static GameManager Instance{
        get { 
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>(); 
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

    private void Update()
    {
        if (isChaosWorld != wasChaosWorld)
        {
            wasChaosWorld = isChaosWorld;
            switchingWorlds = true;
        }
        else
            switchingWorlds = false;

        if (Input.GetButtonDown("Cancel"))
            gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void LateUpdate()
    {
        if(resetWorlds)
           StartCoroutine(ResetWorld());
    }
    // Use this for initialization
    public void GetInventoryItem (string name, Sprite image) {
		inventory.Add (name, image);
		playerUI.SetInventoryImage(inventory[name]);
	}

	public void RemoveInventoryItem (string name) {
		inventory.Remove (name);
		playerUI.SetInventoryImage(playerUI.blankUI);
	}

	public void ClearInventory () {
		inventory.Clear ();
		playerUI.SetInventoryImage(playerUI.blankUI);
	}

    public void SwitchWorld()
    {
        playerUI.animator.SetTrigger("SwitchWorld");
        //isChaosWorld = !isChaosWorld;
    }

    IEnumerator ResetWorld()
    {
        int timer = 0;
        while(timer < 2)
        {
            yield return new WaitForEndOfFrame();
            timer++;
        }
            resetWorlds = false;
    }
}
