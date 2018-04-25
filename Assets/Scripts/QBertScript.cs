using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QBertScript : MonoBehaviour {

	[SerializeField] GameObject BonusPanel;
	[SerializeField] GameObject LosePanel;
	GameObject swearWords;
	GameObject[] QbertIcons;
	GameObject manager;
	GameObject[] cube;
	GameObject[] Reds;

	Animator anim;

	AudioSource[] auds;
	AudioSource swearAud;
	AudioSource getGreen;
	AudioSource winSound;

	Vector3 QbertSpawner;

	float keyCooldown = 0.375f;
	float cubeWidth = 0.15f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	bool enableKey = true;
	bool cooldown = false;
	public bool onElevator = false;
	bool isFall;
	bool isChanged;// = false;
	public bool isGetGreen;

	int lifeShow = 2;
	int life = 3;

	int score;
	Text scoreText;
	Text bonusText;

	int numberOfCubesYellow = 0;



	public void setScore(int scoreAdd){

		score += scoreAdd;
		scoreText.text = ""+score;
	}

	public void IncrementNumberOfCubesTurnedToYellow(){

		numberOfCubesYellow++;

		if (numberOfCubesYellow >= 28)
		{
			setScore (1000);
			gameOver(true);
		}
	}

	void gameOver(bool isWin){
		manager.GetComponent<SpawnBall> ().StopAllCoroutines ();
		manager.GetComponent<SpawnBall> ().enabled = false;
		manager.gameObject.SetActive (false);
		manager.GetComponent<SpawnBall>().CancelInvoke("InstantBall");

		if(isWin)
		{
			winSound.Play ();
			var elevator = GameObject.FindGameObjectsWithTag ("Elevator");
			if(elevator.Length>0){
				setScore (100*elevator.Length);
				BonusPanel.SetActive (true);
				GameObject bonusObj = GameObject.Find ("Bonus");
				bonusText = bonusObj.GetComponent <Text> ();
				bonusText.text = ""+ 100 * elevator.Length;
			}
			for(var i =0; i<cube.Length; i++){
				cube [i].GetComponent <Animator> ().enabled = true;
			}

		}else{
			LosePanel.SetActive (true);
		}

		enableKey = false;
		StartCoroutine(freezeEnemy(1.0f));
		destroyAll ();
		StartCoroutine ("BackToMenu");
	}

	IEnumerator BackToMenu(){
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene ("Menu");
	}

	void Start () {
		auds = this.GetComponents<AudioSource> ();
		swearAud = auds[0];
		getGreen = auds[1];
		winSound = auds [2];

		anim = this.GetComponent<Animator>();
		swearWords = this.transform.GetChild (0).gameObject;
		QbertIcons = GameObject.FindGameObjectsWithTag ("QbertIcon");
		QbertSpawner = this.transform.position;
		GameObject textObj = GameObject.Find ("Score");
		scoreText = textObj.GetComponent<Text>();
		cube = GameObject.FindGameObjectsWithTag ("Cube");
		manager = GameObject.Find ("Manager");
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
				
				swearAud.Play ();
				swearWords.SetActive (true);

				StartCoroutine (pauseGame (1.0f));
				manager.GetComponent<SpawnBall> ().StopAllCoroutines ();
				manager.GetComponent<SpawnBall>().CancelInvoke("InstantBall");

//				destroyAll ();
				this.enabled = false;
				StartCoroutine ("QbertDeath", 0.5f);
			}
		}

		if(other.gameObject.tag == "GreenBall"){
			getGreen.Play ();
			setScore (100);
		
			isGetGreen = true;
			StartCoroutine(freezeEnemy(2.5f));
			Destroy (other.gameObject, 0.25f);
		}
	}
		
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject.tag == "EdgeDrop" && !onElevator){
			isFall = true;
			StartCoroutine ("QbertDeath", 0.5f);
		}
	}

	IEnumerator freezeEnemy(float s){
		yield return new WaitForSeconds(s);
		isGetGreen = false;
	}

	void destroyAll(){
		Reds = GameObject.FindGameObjectsWithTag("RedBall");
		for(var i = 0 ; i < Reds.Length ; i ++){
			Destroy (Reds[i], 0.25f);
		}

		GameObject Coily = GameObject.FindGameObjectWithTag ("Coily");
		if(Coily != null){
			Destroy (Coily, 0.25f);
		}

		var Greens = GameObject.FindGameObjectsWithTag ("GreenBall");
		for(var i = 0 ; i < Greens.Length ; i ++){
			Destroy (Greens[i], 0.25f);
		}
	}

	IEnumerator QbertDeath(){

		destroyAll ();

		life--;

		yield return new WaitForSeconds(0.8f);

		this.gameObject.SetActive (false);

		if(lifeShow>0){
			Destroy (QbertIcons[lifeShow-1]);
			lifeShow--;
			Invoke("QbertReborn",0.5f);
		}

		if(life<=0){
			this.gameObject.SetActive (true);
			gameOver (false);
		}
	}

	void QbertReborn(){
		this.enabled = true;
		this.gameObject.SetActive (true);
		swearWords.SetActive (false);
		manager.GetComponent<SpawnBall> ().reSpawnBall ();
		if(isFall){
			this.GetComponent <BoxCollider2D>().enabled = enabled;
			this.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
			this.gameObject.transform.position = QbertSpawner;
			isFall = false;
		}
	}


	IEnumerator pauseGame(float pauseTime){ 

		Time.timeScale = 0;
		float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1;
	}
		
	void ResetCooldown(){
		cooldown = false;
		enableKey = true;
	}

	IEnumerator slowDown(){
		yield return new WaitForSeconds(5.5f);
	}
		 

	void Update() 
	{	
		if(this.GetComponent <BoxCollider2D>().enabled == !enabled || onElevator || Time.timeScale == 0){
			enableKey = false;
		}else{
			enableKey = true;
		}

		if (enableKey  && numberOfCubesYellow < 28) {
			if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Keypad7)) { //move topf left
				enableKey = false;
				if (!cooldown) {
					
					anim.SetBool ("isUpL", true);
					nextY = transform.position.y + cubeHeight;
					this.GetComponent <BoxCollider2D> ().enabled = !enabled;
					transform.position = new Vector3 (transform.position.x, nextY, 0);
					StartCoroutine (moveQbertUp ("left"));

					Invoke ("ResetCooldown", keyCooldown);
					cooldown = true;
				}
			}	

			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.Keypad9)) { //move top right
				enableKey = false;
				if (!cooldown) {
					
					anim.SetBool ("isUpR", true);
					nextY = transform.position.y + cubeHeight;
					this.GetComponent <BoxCollider2D> ().enabled = !enabled;
					transform.position = new Vector3 (transform.position.x, nextY, 0);
					StartCoroutine (moveQbertUp ("right"));

					Invoke ("ResetCooldown", keyCooldown);
					cooldown = true;
				}
			}

			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.Keypad1)) {  //move bottom left
				enableKey = false;
				if (!cooldown) {
					
					anim.SetBool ("isDownL", true);
					nextX = transform.position.x - cubeWidth;
					this.GetComponent <BoxCollider2D> ().enabled = !enabled;
					transform.position = new Vector3 (nextX, this.transform.position.y, 0);
					StartCoroutine (moveQbertDown ());

					Invoke ("ResetCooldown", keyCooldown);
					cooldown = true;
				}
			}

			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.Keypad3)) { //move bottom right
				enableKey = false;
				if (!cooldown) {
					anim.SetBool ("isDownR", true);
					nextX = transform.position.x + cubeWidth;
					this.GetComponent <BoxCollider2D> ().enabled = !enabled;
					transform.position = new Vector3 (nextX, this.transform.position.y, 0);
					StartCoroutine (moveQbertDown ());

					Invoke ("ResetCooldown", keyCooldown);
					cooldown = true;

				}
			}
		}
	}

	IEnumerator moveQbertDown(){

		yield return new WaitForSeconds(0.0959f);
		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (nextX, nextY, 0);
		this.GetComponent <BoxCollider2D>().enabled = enabled;
	}

	IEnumerator moveQbertUp(string str){

		yield return new WaitForSeconds(0.0959f);

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