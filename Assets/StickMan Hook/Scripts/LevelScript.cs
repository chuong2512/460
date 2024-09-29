using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour 
{
	//public GameObject lockObj;
	private GameObject lvlText;
	public int level_num;
	void Start () 
	{
		lvlText=this.transform.GetChild(0).gameObject;
		lvlText.SetActive(false);
		
		if(level_num<=PlayerPrefs.GetInt("highLevel"))
		{
			lvlText.SetActive(true);
		}
		
	}
	
	void Update ()
	{
		
	}
	public void LoadLevel()
	{
		if(level_num<=PlayerPrefs.GetInt("highLevel"))
		{
			PlayerPrefs.SetInt("levelNum",level_num);
			MenuScript.menu_ref.Loading();
			Invoke("GotoLevel",3f);
		}
	}
	void GotoLevel()
	{
		SceneManager.LoadScene("1");
	}

}
