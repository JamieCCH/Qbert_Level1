using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour {

	AudioSource liftAud;
	GameObject Qbert;
	GameObject Terminal;
	Vector3 destPos;
	public bool isOn = false;

	// Use this for initialization
	void Start () {
		Terminal = GameObject.Find ("Manager");
		Qbert = GameObject.FindGameObjectWithTag ("Player");
		destPos = Terminal.transform.position - gameObject.transform.position;
		liftAud = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isOn){
			transform.Translate(destPos * Time.deltaTime);
			Qbert.transform.Translate (destPos * Time.deltaTime);
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
		yield return new WaitForSeconds(1.8f);
		Destroy (this.gameObject);

		Qbert.transform.position = new Vector3(0.016f, 0.48f, 0);
		isOn = false;
	}

}
