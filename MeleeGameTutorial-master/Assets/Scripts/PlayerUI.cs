using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour {

	[SerializeField] private GameObject startUp;
	public string spawnToObject;
	[SerializeField] private GameObject healthBar, pillBar;
	[SerializeField] private float healthBarWidth;
	private float healthBarWidthSmooth;
	[SerializeField] private float healthBarWidthEase;
	public Animator animator;
	[SerializeField] private Image inventoryItemGraphic;
	public TextMeshProUGUI gemAmountMesh;
	[SerializeField] private float gemAmountSmooth;
	[SerializeField] private float gemAmountEase;
	[SerializeField] private float gemAmountOld;
    public string tutorialText;
    public TextMeshProUGUI tutorialTMP;
	public bool resetPlayer;
	public string loadSceneName;
	public Sprite blankUI;

    public GameObject menu;

	// Use this for initialization
	void Start () {
        healthBarWidth = 1;
		healthBarWidthSmooth = healthBarWidth;
		gemAmountSmooth = (float)GameManager.Instance.gemAmount;
		gemAmountOld = gemAmountSmooth;
	}

	// Update is called once per frame
	void Update () {
		float gemAmount = (float)GameManager.Instance.gemAmount;
		gemAmountMesh.text = Mathf.Round(gemAmountSmooth).ToString();
		gemAmountSmooth += (gemAmount - gemAmountSmooth) * Time.deltaTime * gemAmountEase;
	
		if (gemAmountSmooth >= gemAmountOld) {
			animator.SetTrigger ("getGem");
			gemAmountOld = gemAmountSmooth+1;
		}
        if (GameManager.Instance.gameIsPaused)
            menu.SetActive(true);
        else
            menu.SetActive(false);

        healthBarWidth = ((float)NewPlayer.Instance.stressTolerance + (float)NewPlayer.Instance.curStress) / ((float)NewPlayer.Instance.stressTolerance*2);
		healthBarWidthSmooth += (healthBarWidth - healthBarWidthSmooth) * Time.deltaTime * healthBarWidthEase;
		healthBar.transform.localScale = new Vector2 (healthBarWidthSmooth, healthBar.transform.localScale.y);
	}

	public void HealthBarHurt(){
		animator.SetTrigger ("hurt");
	}

	public void SetInventoryImage(Sprite image){
		inventoryItemGraphic.sprite = image;
	}

    public IEnumerator PlayTutorial()
    {
        yield return new WaitForSeconds(0);
        tutorialTMP.text = tutorialText;
        animator.SetTrigger("PlayTutorial");
    }

    void RestartLevel()
    {
        NewPlayer.Instance.transform.position = GameManager.Instance.checkPoint.transform.position;
        NewPlayer.Instance.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        NewPlayer.Instance.curStress = GameManager.Instance.startChaos; ;
        NewPlayer.Instance.Freeze(false);
        NewPlayer.Instance.canSwitchWorld = true;
        NewPlayer.Instance.headSpriteDark.GetComponent<SpriteRenderer>().enabled = true;
        NewPlayer.Instance.headSpriteLight.GetComponent<SpriteRenderer>().enabled = true;
        GameManager.Instance.isChaosWorld = true;
        GameManager.Instance.resetWorlds = true;
    }

	void LoadScene(){
		startUp.gameObject.tag = "Startup";
		GameManager.Instance.gemAmount = 0;
		GameManager.Instance.ClearInventory ();
        SceneManager.LoadScene(loadSceneName);
        NewPlayer.Instance.transform.position = GameManager.Instance.checkPoint.transform.position;
        if (resetPlayer) {
            NewPlayer.Instance.Freeze (false);
            NewPlayer.Instance.canSwitchWorld = true;
            GameManager.Instance.isChaosWorld = false;
		}
		
		Debug.Log ("Got camera effect component");
	}

    void CoverScreen()
    {
        if (GameManager.Instance.playerIsDead)
        {
            RestartLevel();
        }
        else
        {
            LoadScene();
        }
        GameManager.Instance.playerIsDead = false;
    }

    void SwitchLevel()
    {
        NewPlayer.Instance.cameraEffect.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameManager.Instance.curLevel.camBounds;
        NewPlayer.Instance.cameraEffect.virtualCamera.m_Lens.OrthographicSize = GameManager.Instance.curLevel.camOrthoGraphicSize;
        NewPlayer.Instance.gameObject.transform.position = GameManager.Instance.entrance.position;
        GameManager.Instance.curLevel.gameObject.SetActive(true);
        GameManager.Instance.lastLevel.gameObject.SetActive(false);
        NewPlayer.Instance.Freeze(false);
        GameManager.Instance.resetWorlds = true;
        GameManager.Instance.startChaos = GameManager.Instance.curLevel.startChaos;
    }
}
