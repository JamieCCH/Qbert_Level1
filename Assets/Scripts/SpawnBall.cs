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
//	GameObject newCoily;

	void Awake(){
		float repeatRate = Random.Range (2.5f, 5.5f);
		InvokeRepeating ("InstantBall", 1.5f, repeatRate);
	}

	void Start () {
		
	}
	
	IEnumerator InstantCoily()
	{
		yield return new WaitForSeconds(4.5f);

		Transform spawner;
		int spawnPosi = Random.Range (0,2);

		if(spawnPosi == 1){
			spawner = SpawnerR;
		}else{
			spawner = SpawnerL;
		}

		if(!GameObject.FindGameObjectWithTag ("Coily")){
			GameObject newCoily = Instantiate(Coily, spawner.transform.position, Quaternion.identity) as GameObject;
		}
	}

	void reSpawnBall(){
		if(Ball.gameObject == null){
			float repeatRate = Random.Range (2.0f, 4.5f);
			InvokeRepeating ("InstantBall", 1.5f, repeatRate);
		}
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

		if (spawnRate < 8) { //8
			Ball = Instantiate (RedBall, spawner.transform.position, Quaternion.identity);
		} else {
			Ball = Instantiate (GreenBall, spawner.transform.position, Quaternion.identity);
		}

		Ball.transform.Translate (Vector2.down * 0.35f);

		StartCoroutine (InstantCoily ());
	} 


}
