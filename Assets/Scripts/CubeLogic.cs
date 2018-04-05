//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class CubeLogic : MonoBehaviour {
//
//	public GameObject cube;
//
//	int col = 7;
//	int row = 7;
//	GameObject[,] cubeTile;
////	Transform startPos;
//	float startX = 0.02f;
//	float startY = 0.32f;
//	float cubeMoveX = 0.15f;
//	float cubeMoveY = 0.24f;
//
//	void Start () {
//
////		startPos.position = new Vector3(0.02f, 0.32f, 0.0f);
////		GameObject cubeee = (GameObject)Instantiate(cube, new Vector3(0.02f, 0.32f, 0.0f), Quaternion.identity);
//
////		cubeTile = new GameObject[col,row];
////		for (int i = 0; i < col; i++)
////		{
////			for (int j = 0; j < row; j++)
////			{
////				cubeTile[i,j] = (GameObject)Instantiate(cube, new Vector3(i, j, 0), Quaternion.identity);
////			}
////		}
//
//		GameObject[][] cubeArray = new GameObject[7][];
//		cubeArray[0] = new GameObject[1];
//		cubeArray[1] = new GameObject[2];
////		cubeArray[2] = new GameObject[3];
////		cubeArray[3] = new GameObject[4];
////		cubeArray[4] = new GameObject[5];
////		cubeArray[5] = new GameObject[6];
////		cubeArray[6] = new GameObject[7];
//
//		cubeTile[0,0] = (GameObject)Instantiate(cube, new Vector3(startX, startY, 0.0f), Quaternion.identity);
//
//		for (int i = 0; i < cubeArray.Length; i++)
//		{
//			for (int j = 0; j < cubeArray[i].Length; j++)
//			{
//				cubeTile[i,j] = (GameObject)Instantiate(cube, new Vector3(startX+cubeMoveX, startY+cubeMoveY, 0.0f), Quaternion.identity);
//			}
//		}
//
//	}
//}
