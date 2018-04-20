using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QBertScript : MonoBehaviour {

	GameObject elevator;
	GameObject swearWords;
	GameObject[] QbertIcons;
//	GameObject pauseMenu;

	Animator anim;

	AudioSource[] auds;
	AudioSource swearAud;
	AudioSource getGreen;

	Vector3 QbertSpawner;

	float cubeWidth = 0.15f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	bool enableKey = false;
	bool cooldown = false;
	public bool onElevator = false;
	bool isFall;
//	bool isPause = false;
//	bool QBertCanMove = true;

	int lifeShow = 2;
	int life = 3;
	int colorChangedCount;

	int score;
	Text scoreText;


	void Start () {
		auds = this.GetComponents<AudioSource> ();
		swearAud = auds[0];
		getGreen = auds[1];

		anim = this.GetComponent<Animator>();
		swearWords = this.transform.GetChild (0).gameObject;
		QbertIcons = GameObject.FindGameObjectsWithTag ("QbertIcon");
		QbertSpawner = this.transform.position;
		GameObject textObj = GameObject.Find ("Score");
		scoreText = textObj.GetComponent<Text>();
//		pauseMenu = GameObject.Find ("EscPanel");
//		isPause = pauseMenu.activeSelf;
	}

	void OnCollisionEnter2D (Collision2D other) 
	{

		if (other.gameObject.tag == "Elevator") {
			onElevator = true;
		}else if(other.gameObject.name == "PurpleCube"){
			onElevator = false;
		}

		if(other.gameObject.tag == "Coily" || other.gameObject.tag == "RedBall"){
			if (!onElevator) {
				
				GameObject[] Reds;
				Reds = GameObject.FindGameObjectsWithTag("RedBall");
				for(var i = 0 ; i < Reds.Length ; i ++){
					Destroy (Reds[i], 0.25f);
				}

				GameObject Coily = GameObject.FindGameObjectWithTag ("Coily");
				if(Coily != null){
					Destroy (Coily, 0.25f);
				}

				swearAud.Play ();
				swearWords.SetActive (true);

				StartCoroutine (pauseGame (1.0f,false));

//				Destroy (other.gameObject, 0.5f);
				this.enabled = false;
				StartCoroutine ("QbertDeath", 1.5f);
			}
		}

		if(other.gameObject.tag == "GreenBall"){
			getGreen.Play ();
			Scoring("GreenBall");
			StartCoroutine (pauseGame(3.5f, true));
			Destroy (other.gameObject, 0.25f);

//			StartCoroutine(frozenEnemy());
		}

		if(other.gameObject.tag == "Cube"){
			bool cubeColorChanged = other.gameObject.GetComponent<Animator> ().GetBool ("isHit");
			float cubeAnimSpeed = other.gameObject.GetComponent<Animator> ().speed;
//			if(other.gameObject.name == "PurpleCube (27)" || other.gameObject.name == "PurpleCube (18)" || other.gameObject.name == "PurpleCube (23)" || other.gameObject.name == "PurpleCube (23)")
//			{
//				Debug.Log (cubeColorChanged);
//			}else if (other.gameObject.name == "PurpleCube (14)"){
//				Debug.Log (cubeColorChanged);
//			}
//			Debug.Log (cubeColorChanged);
//			Debug.Log (other.gameObject.name);
//
			if(!cubeColorChanged){
				colorChangedCount++;
				Scoring("Cube");
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject.tag == "EdgeDrop" && !onElevator){
			isFall = true;
			StartCoroutine ("QbertDeath", 0.5f);
		}
	}

//	IEnumerator frozenEnemy(){
//
//	}


	IEnumerator pauseGame(float pauseTime, bool moveQbert){ 

//		QBertCanMove = moveQbert;
		enableKey = moveQbert;

		Time.timeScale = 0;
		float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1;
	}


	void Scoring(string other){
		if(other == "Cube"){
			//PurpleCube (27), PurpleCube (20)", PurpleCube (18)",PurpleCube (23) can't work on count
			score += 25;
			Debug.Log (colorChangedCount);
			if(colorChangedCount == 28){
				score += 1000;
			}
		}

		if(other == "GreenBall"){
			score += 100;
		}

		scoreText.text = ""+score;
	}

	void ResetCooldown(){
		cooldown = false;
	}

	void Update() 
	{	
//		Debug.Log (enableKey);

		if(Input.GetKeyDown (KeyCode.Escape)){
//			isPause = true;
			enableKey = false;
		}
//			
//		if(pauseMenu==null){
////			isPause = false;
//			enableKey = true;
//		}
			
		if(this.GetComponent <BoxCollider2D>().enabled == !enabled || onElevator || Time.timeScale == 0){
			enableKey = false;
//			QBertCanMove = true;
		}else{
			enableKey = true;
		}

		if (enableKey) {
			if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Keypad7)) { //move topf left
				if (!cooldown) {
					anim.SetBool ("isUpL", true);
					nextY = transform.position.y + cubeHeight;
					this.GetComponent <BoxCollider2D> ().enabled = !enabled;
					transform.position = new Vector3 (transform.position.x, nextY, 0);
					StartCoroutine (moveQbertUp ("left"));

					Invoke ("ResetCooldown", 0.5f);
					cooldown = true;
				}
			}	

			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.Keypad9)) { //move top right
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

			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.Keypad1)) {  //move bottom left
				if (!cooldown) {
					anim.SetBool ("isDownL", true);
					this.GetComponent <BoxCollider2D> ().enabled = !enabled;
					nextX = transform.position.x - cubeWidth;
					transform.position = new Vector3 (nextX, transform.position.y, 0);
					StartCoroutine (moveQbertDown ());

					Invoke ("ResetCooldown", 0.5f);
					cooldown = true;
				}
			}

			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.Keypad3)) { //move bottom right
				if (!cooldown) {
					anim.SetBool ("isDownR", true);
					this.GetComponent <BoxCollider2D> ().enabled = !enabled;
					nextX = transform.position.x + cubeWidth;
					transform.position = new Vector3 (nextX, transform.position.y, 0);
					StartCoroutine (moveQbertDown ());

					Invoke ("ResetCooldown", 0.5f);
					cooldown = true;
				}
			}
		}
	}

	IEnumerator QbertDeath(){

		//------------------------------------------------------------------------------------------------
		//!!!!!!!!!!!!!!!!!stop and destroy all enemies---------------------------------------------!!!!!!
		//------------------------------------------------------------------------------------------------
//		enemies [0].GetComponent<CoilyMove> ().enabled = false;
//		enemies [1].GetComponent<MoveBall> ().enabled = false;

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