using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropQbert : MonoBehaviour {

	Collider2D QbertCollider;
	GameObject Qbert;
	AudioSource FallAud;
	bool onElevator;
	bool elevatorGone;
	GameObject elevator;
		
	// Use this for initialization
	void Start () {
		Qbert = GameObject.FindGameObjectWithTag ("Player");
		QbertCollider = Qbert.GetComponent<BoxCollider2D> ();
		FallAud = this.GetComponent<AudioSource>();
		elevator = GameObject.Find ("Elevator");

	}

	// Update is called once per frame
	void Update () {

		// !elevator[0].GetComponent<ElevatorScript>().destoried && 
		if(elevator != null){
			onElevator = elevator.GetComponent<ElevatorScript> ().isOn;
		}

	}

//	void OnCollisionEnter2D (Collision2D c) 
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player" && onElevator == false)
		{
			QbertCollider.enabled = false;
			Qbert.GetComponent<SpriteRenderer>().sortingLayerName = "cube";
			FallAud.Play ();
		}
	}
}
