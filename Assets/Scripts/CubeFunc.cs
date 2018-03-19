using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFunc : MonoBehaviour {

	AudioSource aud;
	Animator anim;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		anim.SetBool ("isHit", false);
		anim.speed = 0;
	}
	
	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Player") {
//			Debug.Log ("OnCollisionEn ter2D");
			aud.Play ();
			anim.speed = 1;
			anim.SetBool ("isHit", true);
		}
	}
		

}
