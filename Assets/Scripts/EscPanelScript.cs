using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EscPanelScript : MonoBehaviour {

	public Button exitBt;
	public Button cancelBt;
	public GameObject EscPanelObj;

	void Start () {

	}

	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Escape))
			EscPanelObj.SetActive (true);

		if(EscPanelObj.activeSelf == true)
		{
			choice (exit,closePanel);
		}

	}

	public void choice(UnityAction exitEvent, UnityAction cancelEvent)
	{
		EscPanelObj.SetActive (true);

		exitBt.onClick.RemoveAllListeners ();
		exitBt.onClick.AddListener (closePanel);
		exitBt.onClick.AddListener (exitEvent);

		cancelBt.onClick.RemoveAllListeners ();
		cancelBt.onClick.AddListener (closePanel);

		AudioSource buttonSound = GetComponent<AudioSource>();
		exitBt.onClick.AddListener(delegate() { buttonSound.Play(); });
		cancelBt.onClick.AddListener(delegate() { buttonSound.Play(); });

	}

	void exit(){
		SceneManager.LoadScene ("Menu");
	}

	void closePanel(){
		if(EscPanelObj.activeSelf == true)
		{
			EscPanelObj.SetActive (false);
		}

	}

}
