using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour {

	GameObject Qbert;
	GameObject Terminal;
	Vector3 destPos;
	bool isOn = false;

	// Use this for initialization
	void Start () {
		Terminal = GameObject.Find ("Manager");
		Qbert = GameObject.FindGameObjectWithTag ("Player");
		destPos = Terminal.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isOn){
			transform.Translate(destPos * Time.deltaTime);
			Qbert.transform.Translate (destPos * Time.deltaTime);
//			Debug.Log ("Termianl pos: " + Terminal.transform.position);
//			Debug.Log ("Elevator pos: " + gameObject.transform.position);
		}
		if(isOn && gameObject.transform.position.y >= 0.65){
			isOn = false;
			Destroy (this.gameObject);
		}
		
	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Player") {
			isOn = true;
		}
	}

}
