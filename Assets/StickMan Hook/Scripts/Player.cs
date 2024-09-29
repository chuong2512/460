using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour 
{
	
	public static Player instance;
	public GameObject[] hooks;
	public Sprite[] hookSprites;
	public float rotationSpeed;
	public HingeJoint2D activeHook;

	public bool isMoving;
	public GameObject currentHook;
	private Rigidbody2D rb2d;
	private LineRenderer line;
	private Animator animator;
	public bool Onground;
	private bool gameOver;
	public GameObject levelComplete,particlePos;
	private GameObject finishEffects;
	Vector2 dir;
	
	public AudioClip winS,failS,swingS;
	private AudioSource audio;

	void Awake()
	{
		instance = this;
	}
	
	void Start () 
	{
			//wonS.GetComponent<AudioSource>().enabled=false;
			//failS.GetComponent<AudioSource>().enabled=false;
		audio=this.GetComponent<AudioSource>();
		levelComplete.SetActive(false);
		line = GetComponent<LineRenderer> ();
		rb2d = this.GetComponent<Rigidbody2D> (); 
		rotationSpeed = 500f;
		animator=this.GetComponent<Animator>();
		animator.Play("Idle");
		gameOver=false;
		finishEffects=GameObject.FindGameObjectWithTag("effect");
		finishEffects.SetActive(false);
	
		
	}

	float angle;

	void Update()
	{ 

		if (Input.GetMouseButtonDown (0)&&!gameOver && !EventSystem.current.IsPointerOverGameObject()) 
		{
			AttachingToHook ();
		}
		if (Input.GetMouseButtonUp (0)&& !EventSystem.current.IsPointerOverGameObject()) 
		{
			dettachingToHook ();
		}

		if (isMoving) {
			line.SetPosition (0, transform.position);
			line.SetPosition (1, currentHook.transform.position);
			audio.PlayOneShot(swingS);
			dir = rb2d.velocity;
				if (dir.y > 0) {  
					transform.RotateAround(currentHook.transform.position, Vector3.forward, dir.x * Time.deltaTime);

					Vector3 diff = currentHook.transform.position - transform.position;
					diff.Normalize();
					float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
					transform.rotation =  Quaternion.Slerp (transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), Time.deltaTime * 10f);  
				}  

		}

		if(!Onground&&!isMoving)
		{
			animator.SetBool("roll",true);
		}
	}
	
	void AttachingToHook()
	{

		if (activeHook != null) 
		{
			activeHook.connectedBody = rb2d; 
			isMoving = true;
			line.enabled = true;
			currentHook = activeHook.gameObject;
			activeHook.gameObject.GetComponent<SpriteRenderer>().sprite=hookSprites[2];
			animator.Play("Swing");
			// dir = rb2d.velocity;
			// angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
			// transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
			// transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z - 90);
			rb2d.velocity = (new Vector2 (rotationSpeed * Time.deltaTime, 0));	
			
		} 
		else 
		{
			if(activeHook)
			{
				activeHook = currentHook.GetComponent<HingeJoint2D> ();
				activeHook.connectedBody = rb2d;
				isMoving = true;
				line.enabled = true;
				activeHook.gameObject.GetComponent<SpriteRenderer>().sprite=hookSprites[2];
				animator.Play("Swing");
				// dir = rb2d.velocity;
				// angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
				// transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
				// transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z - 90);
				rb2d.velocity = (new Vector2 (rotationSpeed * Time.deltaTime, 0));	
			}
			
		}
		
		
	}

	void dettachingToHook()
	{	
		if(currentHook.gameObject!=null&&currentHook.gameObject.GetComponent<HingeJoint2D>())
			currentHook.gameObject.GetComponent<HingeJoint2D>().breakForce=0;
		isMoving = false;
		line.enabled=false;
		if(currentHook)
		{
			currentHook.gameObject.GetComponent<SpriteRenderer>().sprite=hookSprites[0];
		currentHook.gameObject.AddComponent<HingeJoint2D> ();
		}
		
		animator.Play("Idle");
	}

	 
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag=="Base")
		{
			Onground=true;
			animator.SetBool("roll",false);
			Invoke("MakeRoll",0.5f);
			rb2d.AddForce(new Vector2(0,8)*10000*Time.deltaTime);
		}
		else if(other.gameObject.tag=="obstacle")
		{
			//animator.SetBool("roll",true);
			if(!isMoving)
			{
				Invoke("Replay",0.5f);
			//	failS.GetComponent<AudioSource>().enabled=true;
				audio.PlayOneShot(failS);
			}
			
		}
	}

	void MakeRoll()
	{
		Onground=false;
	}

void OnCollisionExit2D(Collision2D other)
{
	if(other.gameObject.tag=="Base")
		{
			//Onground=false;
		}
}


void OnTriggerEnter2D(Collider2D other)
{
	if(other.gameObject.tag=="GameOver"&&!gameOver)
	{
	//	failS.GetComponent<AudioSource>().enabled=true;
	audio.PlayOneShot(failS);
		Invoke("Replay",0.1f);
	}
	else if(other.gameObject.tag=="Finish")
	{
	
			Invoke("LevelComplete",0.5f);
			gameOver = true;
		//	Time.timeScale = 0.2f;
			//rb2d.rotation = (90f);
			//playerSpriteRenderer.sprite = playerFinishSprite;
			rb2d.AddForce (new Vector2 (1f, 0) * rotationSpeed);
			
			//Instantiate (finishEffects.gameObject,particlePos.transform.position, Quaternion.Euler (new Vector3 (0, 90f, 0)));// (transform.position - new Vector3(,0,0))
			
			line.enabled = false;
			
			
			//rb2d.freezeRotation = true;
	}
	
}
/// <summary>
/// Sent when another object leaves a trigger collider attached to
/// this object (2D physics only).
/// </summary>
/// <param name="other">The other Collider2D involved in this collision.</param>
void OnTriggerExit2D(Collider2D other)
{
	if(other.gameObject.tag=="Finish")
	{
		//	wonS.GetComponent<AudioSource>().enabled=true;
		audio.PlayOneShot(winS);
			Invoke("FinishLineEffect",0.02f);
			animator.SetBool("roll",false);
		//	this.transform.eulerAngles=new Vector2(0,0);
			Onground=true;
			if(PlayerPrefs.GetInt("levelNum")>=PlayerPrefs.GetInt("highLevel"))
			{
				PlayerPrefs.SetInt("highLevel",PlayerPrefs.GetInt("highLevel")+1);
			}
		
	}
}

void LevelComplete()
{
	levelComplete.SetActive(true);
	GameManager.game_ref.pauseBtn.SetActive(false);
	GameManager.game_ref.retryBtn.SetActive(false);
}
void Replay()
{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
void FinishLineEffect()
{
	//Instantiate (finishEffects.gameObject,particlePos.transform.position, Quaternion.identity); //transform.position - new Vector3(1,0,0)
	finishEffects.SetActive(true);
	
}

}
