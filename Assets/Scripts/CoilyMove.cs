using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyMove : MonoBehaviour {

	float cubeWidth = 0.155f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;
	bool isBall = true;

	int downCount;
	Animator coilyAnim;
	GameObject Qbert;

	void Start () {
		Qbert = GameObject.Find ("QBert");
		coilyAnim = this.GetComponent <Animator> ();
		transform.Translate (Vector2.down * 0.37f);
		InvokeRepeating ("moveDown", 0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void moveDown(){
		checkDir ();
	}

	void checkDir(){
		int moveDir = Random.Range (0,2);

		switch (moveDir){
		case(0):
//			StartCoroutine(MoveDownRight ());
			StartCoroutine(moveX("left"));
			break;
		case(1):
//			StartCoroutine(MoveDownLeft ());
			StartCoroutine(moveX("right"));
			break;
		}
	}

	IEnumerator moveX(string str){
		yield return new WaitForSeconds(1.2f);

		switch (str){
		case "left":
			nextX = transform.position.x - cubeWidth;
			break;
		case "right":
			nextX = transform.position.x + cubeWidth;
			break;
		}		
		transform.position = new Vector3 (nextX, transform.position.y, 0);

		StartCoroutine(keepDown());

		if (isBall)
			this.GetComponent<Animator> ().Play ("CoilyBall", 0, 0.01f);

	}


	IEnumerator keepDown(){
		
		downCount++;
//		Debug.Log (downCount);

		yield return new WaitForSeconds(0.05f);

		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (transform.position.x, nextY, 0);

		if(downCount>=5){
			isBall = false;
			CancelInvoke ("moveDown");
			Invoke ("chasesQBert", 0.3f);
		}
	}

	void chasesQBert(){
		//check QBert position 
			//Qbert on Left: Qbert X < this.x
		coilyAnim.SetBool ("goUpLeft",true);

			//Qbert on Right: Qbert X > this.x
			//Qbert Higher: Qbert Y > this.y
			//Qbert Lower: Qbert Y < this.y

		//moveUpLeft
		//moveUpRight
		//moveDownLeft
		//moveDownRight
	}


	void checkDirection(){
		
	}


	IEnumerator moveY(string str){

		yield return new WaitForSeconds(0.05f);

		switch (str){
		case "up":
			nextX = transform.position.x - cubeWidth;
			break;
		case "d":
			nextX = transform.position.x + cubeWidth;
			break;
		}		
		transform.position = new Vector3 (nextX, nextY, 0);
	}
}
