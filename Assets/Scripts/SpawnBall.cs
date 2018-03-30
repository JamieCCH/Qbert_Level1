using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour {

	public GameObject GreenBall;
	public GameObject RedBall;
	public GameObject Coily;

	public Transform SpawnerR;
	public Transform SpawnerL;

	GameObject Ball;
	GameObject newCoily;

	// Use this for initialization
	void Start () {
		float repeatRate = Random.Range (7f, 12f);
		InvokeRepeating ("InstantBall", 1.5f, repeatRate);
	}
	
	IEnumerator InstantCoily()
	{
		yield return new WaitForSeconds(3.5f);

		Transform spawner;
		int spawnPosi = Random.Range (0,2);

		if(spawnPosi == 1){
			spawner = SpawnerR;
		}else{
			spawner = SpawnerL;
		}

		if(!GameObject.FindGameObjectWithTag ("Coily")){
			newCoily = Instantiate(Coily, spawner.transform.position, Quaternion.identity) as GameObject;
		}

//		yield return new WaitForSeconds(0.2f);
//		newCoily.transform.Translate (Vector2.down * 0.355f);
	}


	void InstantBall(){
		int spawnRate = Random.Range (0, 10);
		int spawnPosi = Random.Range (0, 2);

		Transform spawner;

		if (spawnPosi == 1) {
			spawner = SpawnerR;
		} else {
			spawner = SpawnerL;
		}

		if (spawnRate < 8) {
			Ball = Instantiate (RedBall, spawner.transform.position, Quaternion.identity);
		} else {
			Ball = Instantiate (GreenBall, spawner.transform.position, Quaternion.identity);
		}

		Ball.transform.Translate (Vector2.down * 0.35f);

		StartCoroutine (InstantCoily ());
	} 


}
