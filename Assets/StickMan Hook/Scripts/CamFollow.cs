using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour 
{

	private GameObject player;
	private Vector3 offSet;

	void Start () {
		player = FindObjectOfType<Player> ().gameObject;	
	}
	

	void Update () {
		transform.position =  Vector3.MoveTowards(transform.position,new Vector3 (player.transform.position.x, transform.position.y, transform.position.z),5f);
	}
}
