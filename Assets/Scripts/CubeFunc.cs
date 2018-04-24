using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeFunc : MonoBehaviour {

	AudioSource aud;
	GameObject Qbert;
	bool isCubeYellow = false;

	[SerializeField] Sprite YelloCube;
	SpriteRenderer spriteRender;

	void Start () {
		spriteRender = gameObject.GetComponent <SpriteRenderer> ();

		aud = this.GetComponent<AudioSource>();
		Qbert = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnCollisionEnter2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			aud.Play ();

			if(!isCubeYellow){
				var qbertPlayer = Qbert.GetComponent <QBertScript> ();
				qbertPlayer.setScore (25);
				qbertPlayer.IncrementNumberOfCubesTurnedToYellow ();
				spriteRender.sprite = YelloCube;
				isCubeYellow = true;
			}

			Qbert.GetComponent <Animator>().SetBool ("isUpL", false);
			Qbert.GetComponent <Animator>().SetBool ("isUpR", false);
			Qbert.GetComponent <Animator>().SetBool ("isDownL", false);
			Qbert.GetComponent <Animator>().SetBool ("isDownR", false);
		}
			
	}
		

}
