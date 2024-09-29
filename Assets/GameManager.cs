using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour 
{
	public static GameManager game_ref;
	public GameObject[] levelObj;
	public GameObject lvlComObj,pauseObj,pauseBtn,retryBtn;
	
	void Start () 
	{
		//Intialize("2910235",true);
		game_ref=this;
		lvlComObj.SetActive(false);
		pauseObj.SetActive(false);
		pauseBtn.SetActive(true);
		retryBtn.SetActive(true);

		for(int i=0;i<levelObj.Length;i++)
		{
			levelObj[i].SetActive(false);
		}
		levelObj[PlayerPrefs.GetInt("levelNum")].SetActive(true);
	}
	
	
	
	void Update () 
	{
		
	}
	public void Next()
	{
		lvlComObj.SetActive(false);
		PlayerPrefs.SetInt("levelNum",PlayerPrefs.GetInt("levelNum")+1);
		for(int i=0;i<levelObj.Length;i++)
		{
			levelObj[i].SetActive(false);
		}
		levelObj[PlayerPrefs.GetInt("levelNum")].SetActive(true);
		SceneManager.LoadScene("1");

	}
	public void GamePause()
	{
		Time.timeScale=0;
		pauseObj.SetActive(true);
		pauseBtn.SetActive(false);
		retryBtn.SetActive(false);
	}
	public void Resume()
	{
		Time.timeScale=1;
		pauseObj.SetActive(false);
		pauseBtn.SetActive(true);
		retryBtn.SetActive(true);
	}
	public void Levels()
	{
		Time.timeScale=1;
		PlayerPrefs.SetInt("MenuCount",1);
		SceneManager.LoadScene("MenuScene");
	}

	public void Home()
	{
		Time.timeScale=1;
		SceneManager.LoadScene("MenuScene");
	}
public void Replay()
{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}


}
