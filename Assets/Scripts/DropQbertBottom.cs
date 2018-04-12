using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropQbertBottom : MonoBehaviour {

	Collider2D QbertCollider;
	GameObject Qbert;
	AudioSource FallAud;


	// Use this for initialization
	void Start () {
		Qbert = GameObject.FindGameObjectWithTag ("Player");
		QbertCollider = Qbert.GetComponent<BoxCollider2D> ();
		FallAud = this.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

	}

//	void OnCollisionEnter2D (Collision2D c)
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			QbertCollider.enabled = false;
			FallAud.Play ();
		}

		if (c.gameObject.tag == "GreenBall" || c.gameObject.tag == "RedBall")
		{
			Destroy (c.gameObject);
		}

	}
}
