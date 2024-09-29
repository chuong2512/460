using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour 
{
	public static MenuScript menu_ref;
	public GameObject menuUI,levelSelection,menuObj,loadingObj;
	void Start () 
	{
			DontDestroyOnLoad(this.gameObject);
		//	DontDestroyOnLoad(menuObj.gameObject);
		//DontDestroyOnLoad(this.GetComponent<AudioSource>());
	
		menu_ref=this;
		MainMenu();
	}
	

	void Update ()
	{
		if(PlayerPrefs.GetInt("MenuCount")==1)
		{
			PlayerPrefs.SetInt("MenuCount",0);
			LevelSelection();
		}
		
	}
	public void MainMenu()
	{
		menuObj.SetActive(true);
		menuUI.SetActive(true);
		levelSelection.SetActive(false);
		loadingObj.SetActive(false);
	}

	public void LevelSelection()
	{
		menuObj.SetActive(false);
		menuUI.SetActive(false);
		levelSelection.SetActive(true);
		loadingObj.SetActive(false);
	}
	public void Loading()
	{
		menuObj.SetActive(true);
		menuUI.SetActive(false);
		levelSelection.SetActive(false);
		loadingObj.SetActive(true);
	}


}
