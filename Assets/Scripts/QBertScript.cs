using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBertScript : MonoBehaviour {

	public GameObject topCube;
	public GameObject rightCube;
	public GameObject leftCube;

	GameObject elevator;
	Animator anim;

	float cubeWidth = 0.15f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	bool enableKey = true;
	bool cooldown = false;
	GameObject swearWords;

	AudioSource swearAud;

	void Start () {
		
		anim = this.GetComponent<Animator>();
		swearAud = this.GetComponent<AudioSource> ();
		swearWords = this.transform.GetChild (0).gameObject;
	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Elevator") {
			enableKey = false;
		}else if(other.gameObject.name == "PurpleCube"){
			enableKey = true;
		}

		if(other.gameObject.tag == "Coily"){
			swearAud.Play ();
			swearWords.SetActive (true);
		}
	}

	void ResetCooldown(){
		cooldown = false;
	}

	void Update() 
	{	
		if(this.GetComponent <BoxCollider2D>().enabled == !enabled || Time.timeScale == 0){
			enableKey = false;
		}else{
			enableKey = true;
		}

		if (enableKey && Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Keypad7)) { //move topf left
			if (!cooldown) {
				anim.SetBool ("isUpL", true);
				nextY = transform.position.y + cubeHeight;
				this.GetComponent <BoxCollider2D> ().enabled = !enabled;
				transform.position = new Vector3 (transform.position.x, nextY, 0);
				StartCoroutine (moveQbertUp ("left"));

				Invoke("ResetCooldown",0.5f);
				cooldown = true;
			}
		}	

		if (enableKey && Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.Keypad9)) { //move top right
			if (!cooldown) {
				anim.SetBool ("isUpR", true);
				nextY = transform.position.y + cubeHeight;
				this.GetComponent <BoxCollider2D> ().enabled = !enabled;
				transform.position = new Vector3 (transform.position.x, nextY, 0);
				StartCoroutine (moveQbertUp ("right"));

				Invoke ("ResetCooldown", 0.5f);
				cooldown = true;
			}
		}

		if (enableKey && Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.Keypad1)) {  //move bottom left
			if (!cooldown) {
				anim.SetBool ("isDownL", true);
				this.GetComponent <BoxCollider2D> ().enabled = !enabled;
				nextX = transform.position.x - cubeWidth;
				transform.position = new Vector3 (nextX, transform.position.y, 0);
				StartCoroutine (moveQbertDown ());

				Invoke("ResetCooldown",0.5f);
				cooldown = true;
			}
		}

		if (enableKey && Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.Keypad3)) { //move bottom right
			if (!cooldown) {
				anim.SetBool ("isDownR", true);
				this.GetComponent <BoxCollider2D> ().enabled = !enabled;
				nextX = transform.position.x + cubeWidth;
				transform.position = new Vector3 (nextX, transform.position.y, 0);
				StartCoroutine (moveQbertDown ());

				Invoke("ResetCooldown",0.5f);
				cooldown = true;
			}
		}




	}

	IEnumerator moveQbertDown(){

		yield return new WaitForSeconds(0.05f);

		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (nextX, nextY, 0);
		this.GetComponent <BoxCollider2D>().enabled = enabled;
	}

	IEnumerator moveQbertUp(string str){

		yield return new WaitForSeconds(0.05f);

		switch (str){
		case "left":
			nextX = transform.position.x - cubeWidth;
			break;
		case "right":
			nextX = transform.position.x + cubeWidth;
			break;
		}
		this.GetComponent <BoxCollider2D>().enabled = enabled;
		transform.position = new Vector3 (nextX, nextY, 0);
	}

}