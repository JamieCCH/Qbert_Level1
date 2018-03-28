using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour {

	public GameObject GreenBall;
	public GameObject RedBall;
	public Transform SpawnerR;
	public Transform SpawnerL;
	GameObject Ball;

	// Use this for initialization
	void Start () {
		float repeatRate = Random.Range (7.5f, 13.5f);
		InvokeRepeating("InstantBall", 1.5f, repeatRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InstantBall(){

		int spawnRate = Random.Range (0,10);
		int spawnPosi = Random.Range (0,2);
		Transform spawner;

//		Debug.Log (spawnRate);
//		Debug.Log (spawnPosi);

		if(spawnPosi == 1){
			spawner = SpawnerR;
		}else{
			spawner = SpawnerL;
		}

		if(spawnRate<8){
			Ball = Instantiate(RedBall, spawner.transform.position, Quaternion.identity);
		}else{
			Ball = Instantiate(GreenBall, spawner.transform.position, Quaternion.identity);
		}

		Ball.transform.Translate (Vector2.down * 0.35f);
	}

}
