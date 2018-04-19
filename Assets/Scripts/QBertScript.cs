using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QBertScript : MonoBehaviour {

	GameObject elevator;
	GameObject swearWords;
	GameObject[] QbertIcons;

	Animator anim;
	AudioSource swearAud;
	Vector3 QbertSpawner;

	float cubeWidth = 0.15f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	bool enableKey = true;
	bool cooldown = false;
	bool onElevator = false;
	bool isFall;

	int lifeShow = 2;
	int life = 3;

	int score;
	Text scoreText;

	void Start () {
		anim = this.GetComponent<Animator>();
		swearAud = this.GetComponent<AudioSource> ();
		swearWords = this.transform.GetChild (0).gameObject;
		QbertIcons = GameObject.FindGameObjectsWithTag ("QbertIcon");
		QbertSpawner = this.transform.position;
		GameObject textObj = GameObject.Find ("Score");
		scoreText = textObj.GetComponent<Text>();
		Debug.Log (scoreText);
	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Elevator") {
			onElevator = true;
		}else if(other.gameObject.name == "PurpleCube"){
			onElevator = false;
		}

		if(other.gameObject.tag == "Coily" || other.gameObject.tag == "RedBall" && !onElevator){
			Destroy (other.gameObject,0.48f);
			this.enabled = false;
			StartCoroutine ("QbertDeath", 0.5f);
			swearAud.Play ();
			swearWords.SetActive (true);
		}

		if(other.gameObject.tag == "Cube"){
			bool cubeColorChanged = other.gameObject.GetComponent<Animator> ().GetBool ("isHit");
			if(!cubeColorChanged){
				score += 25;
				scoreText.text = ""+score;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject.tag == "EdgeDrop" && !onElevator){
			isFall = true;
			Debug.Log ("HI");
			StartCoroutine ("QbertDeath", 0.5f);
		}
	}
		

	void ResetCooldown(){
		cooldown = false;
	}

	void Update() 
	{	
		if(this.GetComponent <BoxCollider2D>().enabled == !enabled || Time.timeScale == 0 ||onElevator){
			enableKey = false;
		}else{
			enableKey = true;
		}

		if (enableKey && Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Keypad7)) { //move topf left
			if (!cooldown) {
				anim.SetBool ("isUpL", true);
				nextY = transform.position.y + cubeHeight;
				this.GetComponent <BoxCollider2D>().enabled = !enabled;
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
				this.GetComponent <BoxCollider2D>().enabled = !enabled;
				transform.position = new Vector3 (transform.position.x, nextY, 0);
				StartCoroutine (moveQbertUp ("right"));

				Invoke ("ResetCooldown", 0.5f);
				cooldown = true;
			}
		}

		if (enableKey && Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.Keypad1)) {  //move bottom left
			if (!cooldown) {
				anim.SetBool ("isDownL", true);
				this.GetComponent <BoxCollider2D>().enabled = !enabled;
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
				this.GetComponent <BoxCollider2D>().enabled = !enabled;
				nextX = transform.position.x + cubeWidth;
				transform.position = new Vector3 (nextX, transform.position.y, 0);
				StartCoroutine (moveQbertDown ());

				Invoke("ResetCooldown",0.5f);
				cooldown = true;
			}
		}
	}

	IEnumerator QbertDeath(){

		//------------------------------------------------------------------------------------------------
		//!!!!!!!!!!!!!!!!!stop and destroy all enemies---------------------------------------------!!!!!!
		//------------------------------------------------------------------------------------------------

		life--;

		yield return new WaitForSeconds(1.0f);

		this.gameObject.SetActive (false);
		if(lifeShow>0){
			Destroy (QbertIcons[lifeShow-1]);
			lifeShow--;
			Invoke("QbertReborn",0.5f);
		}

		if(life<=0){
//			gameOver ();
			Debug.Log ("gameover");
		}
	}

	void QbertReborn(){
		this.enabled = true;
		this.gameObject.SetActive (true);
		swearWords.SetActive (false);

		if(isFall){
			this.GetComponent <BoxCollider2D>().enabled = enabled;
			this.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
			this.gameObject.transform.position = QbertSpawner;
			isFall = false;
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