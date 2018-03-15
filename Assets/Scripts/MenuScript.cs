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


	// Use this for initialization
	void Start () {
		
		AudioSource buttonSound = GetComponent<AudioSource>();

		startBt.onClick.AddListener(delegate() { buttonSound.Play(); });
		exitBt.onClick.AddListener(delegate() { buttonSound.Play(); });
		leaderBt.onClick.AddListener(delegate() { buttonSound.Play(); });

		startBt.onClick.AddListener (()=>SceneManager.LoadScene ("Game"));
		exitBt.onClick.AddListener (exit);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void exit(){
		Debug.Log ("Quit Game!!");
		Application.Quit ();
	}

}
