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
	[SerializeField] private GameObject healthBar, pillBar, blueBar;
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
    public enum FadeType {loadLevel, switchScene}
    [HideInInspector]public FadeType fadeType;

    public GameObject menu;
    public Sprite healthFour, healthSix, healthEight;
    public Sprite blueFour, blueSix, blueEight;
    public Sprite orangeFour, orangeSix, orangeEight;

	// Use this for initialization
	void Start () {
        fadeType = FadeType.switchScene;
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

        if (NewPlayer.Instance.stressTolerance == 2)
        {
            blueBar.GetComponent<Image>().sprite = blueFour;
            blueBar.GetComponent<RectTransform>().localPosition = new Vector3(91.4f, blueBar.GetComponent<RectTransform>().localPosition.y, blueBar.GetComponent<RectTransform>().localPosition.z);
            pillBar.GetComponent<Image>().sprite = healthFour;
            healthBar.GetComponent<Image>().sprite = orangeFour;
            pillBar.GetComponent<RectTransform>().sizeDelta = new Vector2(268, pillBar.GetComponent<RectTransform>().sizeDelta.y);
            healthBarWidth = 27 + 43 * ((float)NewPlayer.Instance.stressTolerance + (float)NewPlayer.Instance.curStress);
            healthBarWidthSmooth += (healthBarWidth - healthBarWidthSmooth) * Time.deltaTime * healthBarWidthEase;
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2( healthBarWidthSmooth, healthBar.GetComponent<RectTransform>().sizeDelta.y);
        }
        else if (NewPlayer.Instance.stressTolerance == 3)
        {
            blueBar.GetComponent<Image>().sprite = blueSix;
            blueBar.GetComponent<RectTransform>().localPosition = new Vector3(46f, blueBar.GetComponent<RectTransform>().localPosition.y, blueBar.GetComponent<RectTransform>().localPosition.z);
            pillBar.GetComponent<Image>().sprite = healthSix;
            pillBar.GetComponent < RectTransform > ().sizeDelta = new Vector2(359, pillBar.GetComponent<RectTransform>().sizeDelta.y);
            healthBar.GetComponent<Image>().sprite = orangeSix;
            healthBarWidth = 27 + 43 * ((float)NewPlayer.Instance.stressTolerance + (float)NewPlayer.Instance.curStress);
            healthBarWidthSmooth += (healthBarWidth - healthBarWidthSmooth) * Time.deltaTime * healthBarWidthEase;
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarWidthSmooth, healthBar.GetComponent<RectTransform>().sizeDelta.y);

        }
        else if (NewPlayer.Instance.stressTolerance == 4)
        {
            blueBar.GetComponent<Image>().sprite = blueEight;
            blueBar.GetComponent<RectTransform>().localPosition = new Vector3(0, blueBar.GetComponent<RectTransform>().localPosition.y, blueBar.GetComponent<RectTransform>().localPosition.z);
            pillBar.GetComponent<Image>().sprite = healthEight;
            pillBar.GetComponent<RectTransform>().sizeDelta = new Vector2(449, pillBar.GetComponent<RectTransform>().sizeDelta.y);
            healthBar.GetComponent<Image>().sprite = orangeEight;
            healthBarWidth = 27 + 43 * ((float)NewPlayer.Instance.stressTolerance + (float)NewPlayer.Instance.curStress);
            healthBarWidthSmooth += (healthBarWidth - healthBarWidthSmooth) * Time.deltaTime * healthBarWidthEase;
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarWidthSmooth, healthBar.GetComponent<RectTransform>().sizeDelta.y);

        }
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
        if(fadeType == FadeType.switchScene)
        {
            NewPlayer.Instance.cameraEffect.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameManager.Instance.curLevel.camBounds;
            NewPlayer.Instance.cameraEffect.virtualCamera.m_Lens.OrthographicSize = GameManager.Instance.curLevel.camOrthoGraphicSize;
            NewPlayer.Instance.cameraEffect.virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = GameManager.Instance.curLevel.camDistance;
            NewPlayer.Instance.gameObject.transform.position = GameManager.Instance.entrance.position;
            GameManager.Instance.curLevel.gameObject.SetActive(true);
            GameManager.Instance.lastLevel.gameObject.SetActive(false);
            NewPlayer.Instance.Freeze(false);
            GameManager.Instance.resetWorlds = true;
            GameManager.Instance.startChaos = GameManager.Instance.curLevel.startChaos;
        }
        else if (fadeType == FadeType.loadLevel)
        {
            SceneManager.LoadScene(loadSceneName);
        }
    }

    void SwitchWorld()
    {
        GameManager.Instance.isChaosWorld = !GameManager.Instance.isChaosWorld;
    }
}
