using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {

	float cubeWidth = 0.155f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	Animator anim;
	string animStateName;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
//		InvokeRepeating("InstantBall", 1.5f, 12.5f);
		InvokeRepeating ("checkDir", 1.5f, 2.5f);
		animStateName = this.gameObject.tag;
	}
		
	void checkDir(){
		int moveDir = Random.Range (0,2);

		switch (moveDir){
		case(0):
			StartCoroutine(startMoveRight ());
			break;
		case(1):
			StartCoroutine(startMoveLeft ());
			break;
		}
	}

	IEnumerator startMoveRight(){
		yield return new WaitForSeconds (1.2f);

		nextX = transform.position.x + cubeWidth;
		transform.position = new Vector3 (nextX, transform.position.y, 0);

		StartCoroutine(keepDown());
		this.GetComponent<Animator> ().Play(animStateName,0,0.01f);
//		this.GetComponent<Animator> ().Play("GreenBall",0,0.01f);

	}

	IEnumerator startMoveLeft(){
		yield return new WaitForSeconds (1.2f);
	
		nextX = transform.position.x - cubeWidth;
		transform.position = new Vector3 (nextX, transform.position.y, 0);

		StartCoroutine(keepDown ());
		this.GetComponent<Animator> ().Play(animStateName,0,0.01f);
//		this.GetComponent<Animator> ().Play("GreenBall",0,0.01f);
	}

	IEnumerator keepDown(){

		yield return new WaitForSeconds(0.05f);

		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (transform.position.x, nextY, 0);
	}

}