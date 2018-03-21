using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {

	public Button startBt;
	public Button leaderBt;
	public Button exitBt;
	public GameObject LeaderBoard;
	public Button closeBt;


	void Start () {
		AudioSource buttonSound = GetComponent<AudioSource>();

		startBt.onClick.AddListener(delegate() { buttonSound.Play(); });
		exitBt.onClick.AddListener(delegate() { buttonSound.Play(); });
		leaderBt.onClick.AddListener(delegate() { buttonSound.Play(); });
		closeBt.onClick.AddListener(delegate() { buttonSound.Play(); });

		startBt.onClick.AddListener (()=>SceneManager.LoadScene ("Game"));
		exitBt.onClick.AddListener (exit);
		leaderBt.onClick.AddListener (showLeaderBoard);
	}
		

	void showLeaderBoard(){
		
		LeaderBoard.SetActive (true);

		if(LeaderBoard.activeSelf == true)
		{
			closeBt.onClick.AddListener (hideBoard);
		}
	}

	void hideBoard(){
		LeaderBoard.SetActive (false);
	}

	void exit(){
		Debug.Log ("Quit Game!!");
		Application.Quit ();
	}

}
