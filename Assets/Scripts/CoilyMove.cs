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

	int downCount;
	Animator coilyAnim;
	GameObject Qbert;
	AudioSource[] coilySounds;


	void Start () {

		coilySounds = this.GetComponents <AudioSource>();
		Qbert = GameObject.Find ("QBert");
		coilyAnim = this.GetComponent <Animator> ();
		transform.Translate (Vector2.down * 0.37f);
		coilySounds[1].Play();
		InvokeRepeating ("moveDown", 0.5f, 2.0f);
	}

	void moveDown(){
		checkDir ();
	}

	void checkDir(){
		int moveDir = Random.Range (0,2);

		switch (moveDir){
		case(0):
			StartCoroutine(moveX("left"));
			break;
		case(1):
			StartCoroutine(moveX("right"));
			break;
		}
	}

	IEnumerator moveX(string str){
		if(isBall)
			yield return new WaitForSeconds(1.2f);
		
		if(!isDown){}
			yield return new WaitForSeconds(0.05f);
		
		if(isDown&&!isBall)
			yield return new WaitForSeconds(0.8f);


		switch (str) {
		case "left":
			nextX = transform.position.x - cubeWidth;
			break;
		case "right":
			nextX = transform.position.x + cubeWidth;
			break;
		}		
		transform.position = new Vector3 (nextX, transform.position.y, 0);

		if (!isDown) {
			StartCoroutine (chasesQBert ());
		} else {
			StartCoroutine (keepDown ());
		}

		if (isBall) {
			coilySounds [1].Play ();
			coilyAnim.Play ("CoilyBall", 0, 0.01f);
		}

	}


	IEnumerator keepDown(){
		
		downCount++;

		yield return new WaitForSeconds(0.15f);

		if(!isBall && canMove)
			coilySounds[0].Play();

		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (transform.position.x, nextY, 0);

		if(downCount>=5){
			isBall = false;
			CancelInvoke ("moveDown");
			StartCoroutine (chasesQBert());
		}
	}
		

	IEnumerator chasesQBert(){

		checkDirection ();

		yield return new WaitForSeconds(1.3f);

		startChase ();
			
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

	void startChase(){
		
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

	IEnumerator moveUp(string str){
		
		yield return new WaitForSeconds (0.7f);
		nextY = transform.position.y + cubeHeight;
		coilySounds [0].Play ();
		transform.position = new Vector3 (transform.position.x, nextY, 0);
		StartCoroutine (moveX (str));

	}

//	void OnCollisionEnter2D (Collision2D c) 
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "CoilyDrop" && !isBall)
		{
//			Debug.Log ("coily drop");
			canMove = false;
			coilySounds[2].Play();
			this.transform.Translate (Vector2.down * 1.4f);
			this.GetComponent<SpriteRenderer>().sortingLayerName = "default";
			Destroy (this.gameObject,1.2f);
		}
	}

}
