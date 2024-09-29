using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour 
{
private Rigidbody2D rb2d;
	
	void Start () {
		rb2d = this.GetComponent<Rigidbody2D> (); 
	}
	

	void Update () {
		
	}
	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag=="Base")
			rb2d.AddForce(new Vector2(0,5)*10000*Time.deltaTime);
	}
}
