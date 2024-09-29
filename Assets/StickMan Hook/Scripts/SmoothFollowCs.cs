
// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;


public class SmoothFollowCs : MonoBehaviour
{
	// The target we are following
	public Transform player, playerhead;
	// The distance in the x-z plane to the target
	public float distance = 1.0f;
	// the height we want the camera to be above the target
	public float height = 2.0f;
	//5
	// How much we
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	
	public GameObject wantedBike;

	public static SmoothFollowCs mee;

	private float Fardistance, Normaldistance;

	public bool Islongview, Islongview2, Islongview3;


	public GameObject RainParticles;
	public GameObject SnowParticles;


	void Awake ()
	{
		mee = this;

	}


	void Start ()
	{        
//		Debug.LogError ("CameraActivated");

	

		//Invoke("AssignNewTargObject",0.1f);

		Fardistance = distance * 1.5f;
		Normaldistance = distance;
	
	}

	void AssignNewTargObject ()
	{
		GameObject gob = new GameObject ();
		gob.transform.position = player.position - Vector3.left * 1.5f;
		gob.transform.parent = player;
		gob.transform.localEulerAngles = new Vector3 (10, 15, 0);
		player = gob.transform;




	}

	private float bba = 0.65f;

	void LateUpdate ()
	{	
		
		Cam ();

		if (Islongview) {
			distance = Mathf.Lerp (distance, Fardistance, 1 * Time.deltaTime);
			angle = Mathf.Lerp (angle, 10, 1 * Time.deltaTime);
			
		} else if (Islongview2) {
			distance = Mathf.Lerp (distance, (Fardistance * 1.5f), 1 * Time.deltaTime);
			angle = Mathf.Lerp (angle, 10, 1 * Time.deltaTime);
			
		} else if (Islongview3) {
			distance = Mathf.Lerp (distance, (Fardistance * 1.5f), 0.5f * Time.deltaTime);
			angle = Mathf.Lerp (angle, 40, 0.5f * Time.deltaTime);
			
		} else {
			distance = Mathf.Lerp (distance, Normaldistance, 1 * Time.deltaTime);
			angle = Mathf.Lerp (angle, 20, 1 * Time.deltaTime);
			
		}

	}

	public static float angle = 20;
	//-70;

//	public GameObject Target1;


	void Cam ()
	{

		if (!player)
			return;

		//Debug.Log ("holded " + RBike.obj.holded);


		//if (GameController.isLevelCompleted) { return; }

		float wantedRotationAngle = player.eulerAngles.y + angle;

		float wantedHeight = player.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
        Vector3 pos = player.position;

		//if (RBike.obj.holded != 1) {
        pos -= currentRotation * Vector3.forward * distance ;
		pos.y = currentHeight;
        transform.position = pos ;
		//	}

		transform.LookAt (player);

		//transform.LookAt (Target1);




	}
	
}




