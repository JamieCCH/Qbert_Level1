using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {

	float cubeWidth = 0.155f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	string animStateName;
	AudioSource JumpAud;

	bool canMove;


	// Use this for initialization
	void Start () {
		JumpAud = this.GetComponent<AudioSource>();
		canMove = true;
		Invoke ("checkDir", 0.5f);
		animStateName = this.gameObject.tag;
		JumpAud.Play ();
	}
		
	void checkDir(){
		if (canMove) {
			int moveDir = Random.Range (0, 2);

			switch (moveDir) {
			case(0):
				StartCoroutine (startMoveRight ());
				break;
			case(1):
				StartCoroutine (startMoveLeft ());
				break;
			}
		}else{
			Time.timeScale = 0;
		}
	}


	IEnumerator startMoveRight(){
		
		if (canMove) {
			yield return new WaitForSeconds (0.5f);

			nextX = this.transform.position.x + cubeWidth;
			this.transform.position = new Vector3 (nextX, this.transform.position.y, 0);

			StartCoroutine (keepDown ());
			this.GetComponent<Animator> ().Play (animStateName, 0, 0.01f);
		}
	}

	IEnumerator startMoveLeft(){
		if (canMove) {
			yield return new WaitForSeconds (0.5f);
	
			nextX = transform.position.x - cubeWidth;
			this.transform.position = new Vector3 (nextX, this.transform.position.y, 0);

			StartCoroutine (keepDown ());
			this.GetComponent<Animator> ().Play (animStateName, 0, 0.01f);
		}
	}

	IEnumerator keepDown(){
		yield return new WaitForSeconds(0.15f);
		nextY = transform.position.y - cubeHeight;
		this.transform.position = new Vector3 (this.transform.position.x, nextY, 0);
		JumpAud.Play ();
		checkDir ();
	}

}