using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class NewPlayer : PhysicsObject {

	public AudioSource audioSource;
	private AnimatorFunctions animatorFunctions;
	private Vector3 origLocalScale;

	public bool frozen = false;
	private float launch = 1;

    public int stressTolerance;
    public int curStress, startStress;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    [SerializeField] private ParticleSystem deathParticles;
	[SerializeField] Vector2 launchPower;
	public GameObject attackHit;


    [HideInInspector] public bool canSwitchWorld = true;
	public bool recovering;
	public float recoveryCounter;
	public float recoveryTime = 2;

	public CameraEffects cameraEffect;

	public string groundType = "grass";
	public AudioClip stepSound;
	public AudioClip jumpSound;
	public AudioClip grassSound;
	public AudioClip stoneSound;

    public GameObject headSpriteLight, headSpriteDark;
	[SerializeField] private float launchRecovery;
	[SerializeField] private GameObject graphic;
	[SerializeField] private Animator animator;
	[SerializeField] private bool jumping;

	public RaycastHit2D ground;


	private static NewPlayer instance;
	public static NewPlayer Instance{
		get 
		{ 
			if (instance == null)
                instance = GameObject.FindObjectOfType<NewPlayer>(); 
			return instance;
		}
	}

	void Start(){
		audioSource = GetComponent<AudioSource>();
		animatorFunctions = GetComponent<AnimatorFunctions>();
		origLocalScale = transform.localScale;
		SetGroundType ();
        canSwitchWorld = true;
    }

    protected override void ComputeVelocity()
    {
        //Switching Worlds
        if (Input.GetButtonDown("SwitchWorld") && canSwitchWorld)
        {
            GameManager.Instance.SwitchWorld();
        }
        
        //Player movement && attack
        Vector2 move = Vector2.zero;

		ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up);
		launch += (0 - launch) * Time.deltaTime*launchRecovery;

        if (!frozen) {
            SwitchHead();
            if (Input.GetButtonDown("Reset"))
            {
                Die();
            }

            move.x = Input.GetAxis ("Horizontal") + launch;
            
            //Jumping
			if (Input.GetButtonDown ("Jump") && grounded) {
				velocity.y = jumpTakeOffSpeed;
				PlayJumpSound ();
				PlayStepSound ();
			}

			if (move.x * GameManager.Instance.everything.transform.localScale.x > 0.01f)
            {
				if (graphic.transform.localScale.x < 0) {
					graphic.transform.localScale = new Vector3 (origLocalScale.x, transform.localScale.y, transform.localScale.z);
				}
			}
            else if (move.x * GameManager.Instance.everything.transform.localScale.x < -0.01f) {
				if (graphic.transform.localScale.x  > 0) {
					graphic.transform.localScale = new Vector3 (-origLocalScale.x , transform.localScale.y, transform.localScale.z);
				}
			}

			//Attack

			if (Input.GetMouseButtonDown (0)) {
				animator.SetTrigger ("attack");
			}
		} else {
			launch = 0;
		}

		if(recovering){
			recoveryCounter += Time.deltaTime;
			if (recoveryCounter >= recoveryTime) {
				recoveryCounter = 0;
				recovering = false;
			}
		}
			
		animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        targetVelocity = move * maxSpeed;
    }

	public void SetGroundType(){
		switch (groundType) {
		case "Grass":
			stepSound = grassSound;
			break;
		case "Stone":
			stepSound = stoneSound;
			break;
		}
	}

	public void Freeze(bool freeze){
		frozen = freeze;
		launch = 0;
	}

	public void PlayStepSound(){
		audioSource.pitch = (Random.Range(0.6f, 1f));
		audioSource.PlayOneShot(NewPlayer.Instance.stepSound);
	}

	public void PlayJumpSound(){
		audioSource.pitch = (Random.Range(0.6f, 1f));
		audioSource.PlayOneShot(NewPlayer.Instance.jumpSound);
	}

    public void SwitchHead()
    {
        if(curStress < 0)
        {
            headSpriteDark.SetActive(false);
            headSpriteLight.SetActive(true);
            headSpriteLight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            headSpriteDark.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else if ( curStress == 0)
        {
            headSpriteDark.SetActive(true);
            headSpriteLight.SetActive(true);
            headSpriteLight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            headSpriteDark.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            headSpriteDark.SetActive(true);
            headSpriteLight.SetActive(false);
            headSpriteLight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            headSpriteDark.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void CollectOrb(int amount)
    {
        curStress += amount;
        if (curStress <= -stressTolerance)
            Die();
        if (curStress >= stressTolerance)
            Die();
    }

    public void CollectMemory()
    {
        stressTolerance += 1;
    }

	public void Hit(int launchDirection, int damage){
		if (!recovering) {
			cameraEffect.Shake (100, 1);
			animator.SetTrigger ("hurt");
			velocity.y = launchPower.y;
			launch = launchDirection * (launchPower.x);
			recoveryCounter = 0;
			recovering = true;
            curStress -= damage;

            if (curStress <= -stressTolerance) 
				Die ();
			if (curStress >= stressTolerance)
                Die();

            GameManager.Instance.playerUI.HealthBarHurt ();
		}
	}

	public void Die(){
        if (!GameManager.Instance.playerIsDead)
        {
            deathParticles.gameObject.SetActive(true);
            deathParticles.Emit(10);
            deathParticles.gameObject.GetComponent<AudioSource>().Play();
            //deathParticles.transform.parent = transform.parent;
            GameManager.Instance.playerUI.animator.SetTrigger("coverScreen");
            GameManager.Instance.playerUI.loadSceneName = SceneManager.GetActiveScene().name;
            GameManager.Instance.playerUI.spawnToObject = "SpawnStart";
            GameManager.Instance.playerUI.resetPlayer = true;
            GameManager.Instance.playerIsDead = true;
            GetComponent<SpriteRenderer>().enabled = false;
            headSpriteDark.GetComponent<SpriteRenderer>().enabled = false;
            headSpriteLight.GetComponent<SpriteRenderer>().enabled = false;
            Freeze(true);
        }
    }

	public void ResetLevel(){
		Freeze (false);
		curStress = startStress;
		deathParticles.gameObject.SetActive (false);
		GetComponent<MeshRenderer> ().enabled = true;
		GameManager.Instance.gemAmount = 0;
		GameManager.Instance.inventory.Clear ();
	}
}