using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropQbertLeft : MonoBehaviour {

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
		elevator = GameObject.Find ("Elevator (1)");
	}

	// Update is called once per frame
	void Update () {

		if(elevator != null){
			onElevator = elevator.GetComponent<ElevatorScript> ().isOn;
		}
	}

	void OnCollisionEnter2D (Collision2D c) 
	{
		if (c.gameObject.tag == "Player" && onElevator == false)
		{
			QbertCollider.enabled = false;
			Qbert.GetComponent<SpriteRenderer>().sortingLayerName = "cube";
			FallAud.Play ();
		}
	}
}
