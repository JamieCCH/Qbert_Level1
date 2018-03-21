using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBertScript : MonoBehaviour {

	public Transform topCube;
	public Transform rightCube;
	public Transform leftCube;
	Transform startPos;
//	Transform rightPos;

	Animator anim;
//	float overTime = 2f;
//	float startTime;
//	bool startMove = false;
//	float journeyLength;

	public float speed = 1.0F;
//	private float startTime;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
//		journeyLength = Vector3.Distance (rightCube.position,topCube.position);
//		startTime = Time.time;
	}

//	void OnCollisionEnter2D (Collision2D other) 
//	{
//		if (other.gameObject.tag == "Cube") {
//			Debug.Log ("OnCollisionEnter2D");
//		}
//	}


	void Update() 
	{
//		Vector2 distDownRight = rightCube.position - topCube.position;
//		Vector2 distDownLeft = leftCube.position - topCube.position ;
//		Vector2 distUpRight = distDownRight * -1;
//		Vector2 distUpLeft = distDownLeft * -1;

//		float distCovered = (Time.time - startTime) * speed;
//		float fracJourney = distCovered / journeyLength;
//		transform.position = Vector3.Lerp(topCube.position, rightCube.position, fracJourney);


		if(Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Keypad7)) //move up left
			transform.position -= rightCube.position - topCube.position;
		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Keypad7)) //move up right
			transform.position -= leftCube.position - topCube.position;
		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.Keypad7)) //move down left
			transform.position += leftCube.position - topCube.position;

		if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyUp(KeyCode.Keypad7))
		{
			transform.position += rightCube.position - topCube.position;
			anim.SetBool ("isDownR",true);
//			startTime = Time.time;
//			journeyLength = Vector3.Distance (topCube.position, rightCube.position);
//			startMove = true;	
		}

		if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Keypad7)) //move down right
		{
			anim.SetBool ("isDownR",false);
		}



//		if(startMove){
//			float distCovered = (Time.time - startTime)*1f;
//			float fracJourney = distCovered / journeyLength;
//			transform.position = Vector3.Slerp (startPos.position, rightPos.position, fracJourney);
//			anim.SetBool ("isDownR",false);
//			StartCoroutine(moveQbert(rightCube));
		}

//		if(transform.position==rightCube.position){
//			startMove = false;
////			anim.SetBool ("isDownR",false);
//			Vector3 vector = rightPos.position - startPos.position;
//			startPos = rightPos;
//			rightPos.position = startPos.position + (vector.normalized * journeyLength);
//		}

//	}

//	IEnumerator moveQbert(Transform target)
//	{
////		while(Vector3.Distance (transform.position, target.position)>0.05f)
////		{
//			anim.SetBool ("isDownR",true);
//			float distCovered = (Time.time - startTime)*1f;
//			float fracJourney = distCovered / journeyLength;
//			transform.position = Vector3.Lerp (startPos.position, target.position, fracJourney);
//
//			Debug.Log("Reached the target.");
//			yield return null;
////		}
//
//		if(distCovered >= fracJourney){
//			yield return new WaitForSeconds(1f);
//
//			Debug.Log("MyCoroutine is now finished.");
//
//			startMove = false;
//			anim.SetBool ("isDownR",false);
//
////			startPos = rightPos;
////			rightPos.position = startPos.position + (vector.normalized * journeyLength);
//		}
//
//
//		//isDownR = true;
////		anim.SetBool ("isDownR",true);
//	}

}