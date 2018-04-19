﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeFunc : MonoBehaviour {

	AudioSource aud;
	Animator anim;
	GameObject Qbert;


	void Start () {
		aud = this.GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		anim.SetBool ("isHit", false);
		anim.speed = 0;
		Qbert = GameObject.FindGameObjectWithTag ("Player");

	}

	void Update(){
		
	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			aud.Play ();
			anim.speed = 1;
			anim.SetBool ("isHit", true);

			Qbert.GetComponent <Animator>().SetBool ("isUpL", false);
			Qbert.GetComponent <Animator>().SetBool ("isUpR", false);
			Qbert.GetComponent <Animator>().SetBool ("isDownL", false);
			Qbert.GetComponent <Animator>().SetBool ("isDownR", false);
		}


	}
		

}
