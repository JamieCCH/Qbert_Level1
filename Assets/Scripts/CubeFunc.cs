using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFunc : MonoBehaviour {

	AudioSource aud;
	Animator anim;
	GameObject Qbert;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		anim.SetBool ("isHit", false);
		anim.speed = 0;
		Qbert = GameObject.FindGameObjectWithTag ("Player");

	}

	void Update(){
//		Vector3 down = transform.TransformDirection(Vector3.down) * 0.5f;
//		QbertRay = new Ray(Qbert.transform.position, down);
//		RaycastHit2D hit;

//		RaycastHit2D hit = Physics2D.Raycast(Qbert.transform.position, Vector2.down, 0.5f);
//		if (hit.point != null) {
//			Debug.Log (hit.transform.name);
//		}

//		if(Physics2D.Raycast(Qbert.transform.position, Vector2.down, out hit, 0.5f))
//		{
//			if(hit.collider == null)
//			Debug.Log (hit.transform.name);
//			Debug.Log ("null");
//		}	


		Debug.DrawRay(Qbert.transform.position, Vector2.down, Color.green);

//		RaycastHit2D hit = Physics.Raycast(Qbert.transform.position, Vector2.down);

//		Debug.Log (hit.transform.name);

	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Player") {

//			Debug.Log (this.name);

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
