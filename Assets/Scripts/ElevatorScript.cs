using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour {

	AudioSource liftAud;
	GameObject Qbert;
	GameObject Terminal;
	Vector3 destPos;
	public bool isOn;
	public bool destoried;

	// Use this for initialization
	void Start () {
		Terminal = GameObject.Find ("Manager");
		Qbert = GameObject.FindGameObjectWithTag ("Player");
		destPos = Terminal.transform.position - gameObject.transform.position;
		liftAud = this.GetComponent<AudioSource>();
		destoried = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(isOn){
			transform.Translate(destPos * Time.deltaTime);
			Qbert.transform.Translate (destPos * Time.deltaTime);
//			Debug.Log ("Termianl pos: " + Terminal.transform.position);
//			Debug.Log ("Elevator pos: " + gameObject.transform.position);
		}
		if(isOn && gameObject.transform.position.y >= Terminal.transform.position.y){
			isOn = false;
			StartCoroutine(DestroyElevator ());
			Qbert.GetComponent<Animator> ().SetTrigger ("default");
		}
		
	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Player") {
			isOn = true;
			liftAud.Play ();
		}
	}

	IEnumerator DestroyElevator(){
		yield return new WaitForSeconds(0.5f);
		Destroy (this.gameObject);
		isOn = false;
		destoried = true;
		Qbert.transform.position = new Vector3(0.016f, 0.48f, 0);
	}

}
