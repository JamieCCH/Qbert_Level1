using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBertScript : MonoBehaviour {

	public GameObject topCube;
	public GameObject rightCube;
	public GameObject leftCube;

//	Vector3 distDownRight;
//	Vector3 distDownLeft;
//	Vector3 distUpRight;
//	Vector3 distUpLeft;
//
//	Transform startPos;
//	Transform endPos;

	Animator anim;

//	bool startMove = false;

//	float journeyLength;
//	float speed = 0.25f;
//	float startTime;

//	float distCovered;
//	float fracJourney;

	float cubeWidth = 0.15f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	void Start () {
		anim = GetComponent<Animator>();

//		distDownRight = rightCube.transform.position - topCube.transform.position;
//		distDownLeft = leftCube.transform.position - topCube.transform.position ;
//		distUpRight = topCube.transform.position - leftCube.transform.position;
//		distUpLeft = topCube.transform.position - rightCube.transform.position;
//
//		startPos = topCube.gameObject.transform;
//		endPos = rightCube.gameObject.transform;
//		Debug.Log("endPos.position1 : " + endPos.position);
//		Debug.Log("rightCube.position1 : " + rightCube.gameObject.transform.position);
	}

//	void OnCollisionEnter2D (Collision2D other) 
//	{
//		if (other.gameObject.name == "Elevator") {
//			Debug.Log ("OnCollisionEnter2D");
//		}
//	}


	void Update() 
	{
		if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Keypad7)) //move topf left
		{
			anim.SetBool ("isUpL", true);
			nextY = transform.position.y + cubeHeight;
			transform.position = new Vector3 (transform.position.x, nextY, 0);
			StartCoroutine (moveQbertUp("left"));
//			transform.position += distUpLeft;
//			transform.Translate (distUpLeft*speed);
		}	

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Keypad9)) //move top right
		{
			anim.SetBool ("isUpR",true);
//			transform.position += distUpRight;
//			transform.Translate (distUpRight*speed);

			nextY = transform.position.y + cubeHeight;
			transform.position = new Vector3 (transform.position.x, nextY, 0);
			StartCoroutine (moveQbertUp("right"));
		}
	
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad1))  //move bottom left
		{
			anim.SetBool ("isDownL",true);
//			transform.position += distDownLeft;
//			transform.Translate (distDownLeft*speed);

			nextX = transform.position.x - cubeWidth;
			transform.position = new Vector3 (nextX, transform.position.y, 0);
			StartCoroutine (moveQbertDown());

		}
		if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.Keypad3)) //move bottom right
		{
			anim.SetBool ("isDownR",true);
			nextX = transform.position.x + cubeWidth;
			transform.position = new Vector3 (nextX, transform.position.y, 0);
//			distDownRight = new Vector3 (nextX, transform.position.y, 0);
//			transform.Translate (distDownRight * 0.35f);
			StartCoroutine (moveQbertDown());
//			startTime = Time.time;
//			startMove = true;

//			endPos.position = new Vector3 (nextX, nextY, 0);

//			if(transform.position.x != 0 && transform.position.y != 0.32f){
//				nextX = endPos.position.x + cubeWidth;
//				nextY = endPos.position.y + cubeHeight;
//				endPos.position = new Vector3 (nextX, nextY, 0);
//				journeyLength = Vector3.Distance (endPos.position,topCube.transform.position);
//
				
//				startPos = endPos;
//			}

//			Debug.Log("endPos.position2 : " + endPos.position);
//			Debug.Log("rightCube.position2 : " + rightCube.gameObject.transform.position);

		}

//		if(startMove){
//			StartCoroutine (moveQbert());
//			anim.SetBool ("isDownR",true);
//			distCovered = (Time.time - startTime) * speed;
//			fracJourney = distCovered / journeyLength;
//			startPos.transform.position = gameObject.transform.position;
//			endPos.transform.position = distDownRight;
//			transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
//		}



	} //updated ends

	IEnumerator moveQbertDown(){

//		switch (str){
//		case "left":
//			nextX = transform.position.x - cubeWidth;
//			break;
//		case "down":
//			nextY = transform.position.y - cubeHeight;
//			break;
//		}

		yield return new WaitForSeconds(0.05f);
//		distCovered = (Time.time - startTime) * speed;
//		fracJourney = distCovered / journeyLength;
//		transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (nextX, nextY, 0);
//		transform.Translate (distDownRight * 0.35f);
//		anim.SetBool ("isDownR",false);
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

		transform.position = new Vector3 (nextX, nextY, 0);
	}

//	IEnumerator moveQbert()
//	{
//		while(Vector3.Distance (transform.position, endPos.position)>0.05f)
//		{
//			anim.SetBool ("isDownR",true);
//
//			distCovered = (Time.time - startTime) * speed;
//			fracJourney = distCovered / journeyLength;
//			transform.position = Vector3.Lerp(startPos.position, endPos.position, 0.9f);
//
//			Debug.Log("Reached the target.");
//			yield return null;
//		}
//
//		if(distCovered >= fracJourney){
//			yield return new WaitForSeconds(1f);
//
//			Debug.Log("MyCoroutine is now finished.");
//
//			startMove = false;
//			anim.SetBool ("isDownR",false);
//
//			startPos = rightPos;
//			rightPos.position = startPos.position + (vector.normalized * journeyLength);
//		}
//				
//	}

	}