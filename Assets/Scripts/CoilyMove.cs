using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyMove : MonoBehaviour {

	float cubeWidth = 0.155f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;
	bool isBall = true;
	bool isDown = true;
	bool isRight;
	bool canMove = true;
	bool QBertOnElevator = true;
	bool isFrozen;

	int downCount;
	Animator coilyAnim;
	GameObject Qbert;
	GameObject manager;

	AudioSource[] coilySounds;

	void Start () {
		coilySounds = this.GetComponents <AudioSource>();
		Qbert = GameObject.FindGameObjectWithTag ("Player");
		coilyAnim = this.GetComponent <Animator> ();
		transform.Translate (Vector2.down * 0.37f);
		coilySounds[1].Play();
		InvokeRepeating ("moveDown", 0.5f, 0.8f);
		QBertOnElevator = Qbert.GetComponent<QBertScript> ().onElevator;
		manager = GameObject.Find ("Manager");
	}

	void Update(){

		var QbertStatus = Qbert.GetComponent <QBertScript> ();
		isFrozen = QbertStatus.isGetGreen;

		if(isFrozen){
			canMove = false;
		}else{
			canMove = true;
		}

//		if(Input.GetKeyDown (KeyCode.Z)){
//			canMove = false;
//		}else if(Input.GetKeyDown (KeyCode.C)){
//			canMove = true;
//		}

		if(!isBall && this.transform.position.y <= -0.88){
			isDown = false;
		}

		if(isBall && !canMove){
			CancelInvoke ("moveDown");
		}

		if(isBall && !canMove){
			InvokeRepeating ("moveDown", 0.5f, 0.8f);
		}

		if(isBall && this.transform.position.y <= -0.88){
			isBall = false;
			CancelInvoke ("moveDown");
			StartCoroutine (chasesQBert());
		}
	}

	void moveDown(){
		checkDir ();
	}

	void checkDir(){
		int moveDir = Random.Range (0, 2);
		switch (moveDir) {
		case(0):
			StartCoroutine (moveX ("left"));
			break;
		case(1):
			StartCoroutine (moveX ("right"));
			break;
		}
	}

	IEnumerator moveX(string str){

		while (!canMove)
		{
			yield return null;
		}

		if(isBall){
			yield return new WaitForSeconds(0.5f);
			coilySounds [1].Play ();
			coilyAnim.Play ("CoilyBall", 0, 0.01f);
		}
			
		if(!isDown){}
			yield return new WaitForSeconds(0.05f);
		
		if(isDown&&!isBall)
			yield return new WaitForSeconds(0.4f);


		switch (str) {
		case "left":
			nextX = transform.position.x - cubeWidth;
			break;
		case "right":
			nextX = transform.position.x + cubeWidth;
			break;
		}		
		transform.position = new Vector3 (nextX, transform.position.y, 0);

//		if (!isDown && canMove) {
		if (!isDown) {
			StartCoroutine (chasesQBert ());
		} else {
			StartCoroutine (keepDown ());
		}
			
//		if (isBall && canMove) {
//		if (isBall) {
//			coilySounds [1].Play ();
//			coilyAnim.Play ("CoilyBall", 0, 0.01f);
//		}
	}


	IEnumerator keepDown(){

		while (!canMove)
		{
			yield return null;
		}
		
		downCount++;

		yield return new WaitForSeconds(0.15f);

		if(!isBall){
			coilySounds[0].Play();
			StartCoroutine (chasesQBert());
		}
			
		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (transform.position.x, nextY, 0);

//		if(isBall && this.transform.position.y <= -0.88){
//			isBall = false;
//			CancelInvoke ("moveDown");
//			StartCoroutine (chasesQBert());
//		}

	}
		

	IEnumerator chasesQBert(){

		while (!canMove)
		{
			yield return null;
		}

		QBertOnElevator = Qbert.GetComponent<QBertScript> ().onElevator;

		checkDirection ();

		if(QBertOnElevator){
			yield return new WaitForSeconds(1.9f);
		}else{
			yield return new WaitForSeconds(0.5f);
		}

//		if(canMove)
//		startChase ();
		StartCoroutine (startChase ());
			
	}


	void checkDirection(){

		float QbertX = Qbert.transform.position.x;
		float QbertY = Qbert.transform.position.y;
		float CoilyX = this.transform.position.x;
		float CoilyY = this.transform.position.y;

		if(QbertX < CoilyX && QbertY > CoilyY) //moveUpLeft
		{
			isDown = false;
			isRight = false;
		}

		if(QbertX > CoilyX && QbertY > CoilyY ) //moveUpRight
		{
			isDown = false;
			isRight = true;
		}

		if(QbertX > CoilyX && QbertY < CoilyY)//moveDownRight
		{
			isDown = true;
			isRight = true;
		}
		if(QbertX < CoilyX && QbertY < CoilyY)//moveDownLeft
		{
			isDown = true;
			isRight = false;
		}
	}

//	void startChase(){
	IEnumerator startChase(){

		while (!canMove)
		{
			yield return null;
		}

//		if (canMove && !isBall) {
		if (!isBall){
			if (!isDown && !isRight) {
				StartCoroutine (moveUp ("left"));
				coilyAnim.Play ("CoilyUpLeft", 0, 0);
			}

			if (!isDown && isRight) {
				StartCoroutine (moveUp ("right"));
				coilyAnim.Play ("CoilyUpRight", 0, 0);

			}

			if (isDown && isRight) {
				StartCoroutine (moveX ("right"));
				coilyAnim.Play ("CoilyRight", 0, 0);
			}

			if (isDown && !isRight) {
				StartCoroutine (moveX ("left"));
				coilyAnim.Play ("CoilyLeft", 0, 0);
			}
		}
	}

	IEnumerator moveUp(string str){

		while (!canMove)
		{
			yield return null;
		}

		yield return new WaitForSeconds (0.5f);
		nextY = transform.position.y + cubeHeight;
		coilySounds [0].Play ();
		transform.position = new Vector3 (transform.position.x, nextY, 0);
		StartCoroutine (moveX (str));

	}
		

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "CoilyDrop" && !isBall)
		{
			coilySounds[2].Play();
			this.transform.Translate (Vector2.down * 1.4f);
			canMove = false;

			GameObject[] Reds;
			Reds = GameObject.FindGameObjectsWithTag("RedBall");
			for(var i = 0 ; i < Reds.Length ; i ++){
				Destroy (Reds[i], 0.05f);
			}

			var qbertPlayer = Qbert.GetComponent <QBertScript> ();
			qbertPlayer.setScore (500);

			manager.GetComponent<SpawnBall> ().StopAllCoroutines ();
			manager.GetComponent<SpawnBall> ().enabled = false;
			manager.gameObject.SetActive (false);
			manager.GetComponent<SpawnBall>().CancelInvoke("InstantBall");
		
			Invoke("reSpawnBalls", 2f);

			this.GetComponent<SpriteRenderer>().sortingLayerName = "default";
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		QBertOnElevator = Qbert.GetComponent<QBertScript> ().onElevator;
		if(c.gameObject.tag == "Player" && !QBertOnElevator){
			coilySounds[3].Play();
		}
	}

	void reSpawnBalls(){
		
		manager.gameObject.SetActive (true);
		manager.GetComponent<SpawnBall> ().enabled = true;
		manager.GetComponent<SpawnBall> ().reSpawnBall ();
//		manager.GetComponent<SpawnBall> ().Invoke ("reSpawnBall",1);
		Destroy (this.gameObject, 0.5f);
	}
}