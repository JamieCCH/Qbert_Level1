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

	GameObject elevator;
	Animator anim;

	float cubeWidth = 0.15f;
	float cubeHeight = 0.24f;
	float nextX;
	float nextY;

	bool enableKey = true;

	void Start () {
		
		anim = GetComponent<Animator>();

//		distDownRight = rightCube.transform.position - topCube.transform.position;
//		distDownLeft = leftCube.transform.position - topCube.transform.position ;
//		distUpRight = topCube.transform.position - leftCube.transform.position;
//		distUpLeft = topCube.transform.position - rightCube.transform.position;

	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Elevator") {
			enableKey = false;
		}else if(other.gameObject.name == "PurpleCube"){
			enableKey = true;
		}
	}


	void Update() 
	{	
		if (enableKey && Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Keypad7)) //move topf left
		{
			anim.SetBool ("isUpL", true);
			nextY = transform.position.y + cubeHeight;
			this.GetComponent <BoxCollider2D>().enabled = !enabled;
			transform.position = new Vector3 (transform.position.x, nextY, 0);
			StartCoroutine (moveQbertUp("left"));
//			transform.position += distUpLeft;
//			transform.Translate (distUpLeft*speed);
		}	

		if (enableKey && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Keypad9)) //move top right
		{
			anim.SetBool ("isUpR",true);
//			transform.position += distUpRight;
//			transform.Translate (distUpRight*speed);

			nextY = transform.position.y + cubeHeight;
			this.GetComponent <BoxCollider2D>().enabled = !enabled;
			transform.position = new Vector3 (transform.position.x, nextY, 0);
			StartCoroutine (moveQbertUp("right"));
		}
	
		if (enableKey && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad1))  //move bottom left
		{
			anim.SetBool ("isDownL",true);
//			transform.position += distDownLeft;
//			transform.Translate (distDownLeft*speed);
			this.GetComponent <BoxCollider2D>().enabled = !enabled;
			nextX = transform.position.x - cubeWidth;
			transform.position = new Vector3 (nextX, transform.position.y, 0);
			StartCoroutine (moveQbertDown());
		}

		if(enableKey && Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.Keypad3)) //move bottom right
		{
			anim.SetBool ("isDownR",true);
			this.GetComponent <BoxCollider2D>().enabled = !enabled;
			nextX = transform.position.x + cubeWidth;
			transform.position = new Vector3 (nextX, transform.position.y, 0);
//			distDownRight = new Vector3 (nextX, transform.position.y, 0);
//			transform.Translate (distDownRight * 0.35f);
			StartCoroutine (moveQbertDown());
		}

	}

	IEnumerator moveQbertDown(){

		yield return new WaitForSeconds(0.05f);

		nextY = transform.position.y - cubeHeight;
		transform.position = new Vector3 (nextX, nextY, 0);
		this.GetComponent <BoxCollider2D>().enabled = enabled;
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
		this.GetComponent <BoxCollider2D>().enabled = enabled;
		transform.position = new Vector3 (nextX, nextY, 0);
	}

}