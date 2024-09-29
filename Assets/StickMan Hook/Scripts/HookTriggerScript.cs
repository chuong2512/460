using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTriggerScript : MonoBehaviour 
{
	public static HookTriggerScript instance;
	public GameObject nextHook;
	void Awake(){
		instance = this;
	}
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		transform.GetChild (0).gameObject.SetActive (false);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			for (int i = 0; i < Player.instance.hooks.Length; i++) 
			{
				if (this.gameObject.name.Contains("Hook"))
				{
					nextHook = this.gameObject;
				
					Player.instance.activeHook = nextHook.GetComponent<HingeJoint2D> ();
				} 
			}

			transform.GetChild (0).gameObject.SetActive (true);
			this.GetComponent<SpriteRenderer>().sprite=Player.instance.hookSprites[1];
			//Player.instance.inAir=false;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			//nextHook = null;
			transform.GetChild (0).gameObject.SetActive (false);
			this.GetComponent<SpriteRenderer>().sprite=Player.instance.hookSprites[0];
			//Player.instance.inAir=true;
		}
	}
}
