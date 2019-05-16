using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	enum ItemType {InventoryItem, Gem, Orb, Memory};
    [SerializeField] private SwitchOnWorldState orbParent;
	[SerializeField] ItemType itemType;
	[SerializeField] private AudioClip collectSound;
	[SerializeField] private AudioClip bounceSound;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private string itemName;
	[SerializeField] private int itemAmount;
    [SerializeField] private int yinYangAmount;
	[SerializeField] private Sprite image;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == NewPlayer.Instance.gameObject) {
			Collect ();
		}
	}

	void Collect(){
		if(itemType == ItemType.InventoryItem)
        {
			if(itemName != ""){
				GameManager.Instance.GetInventoryItem(itemName, image);
			}
		}
        else if(itemType == ItemType.Gem)
        {
			GameManager.Instance.gemAmount += itemAmount;
		}
        else if (itemType == ItemType.Orb)
        {
            NewPlayer.Instance.CollectOrb(yinYangAmount);
        }
        else if (itemType == ItemType.Memory)
        {
            NewPlayer.Instance.CollectMemory();
        }

        GameManager.Instance.audioSource.PlayOneShot (collectSound);

        orbParent.isCollected = true;
	}
}
