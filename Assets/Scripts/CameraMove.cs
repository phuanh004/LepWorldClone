﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	// private Vector2 velocity;

	// public float smoothTimeX;
	// public float smoothTimeY;

	// public GameObject player;

	// public bool bounds;

	// public Vector3 minCameraPos;
	// public Vector3 maxCameraPos;

	// // Use this for initialization
	// void Start () {
	// 	player = GameObject.FindGameObjectWithTag ("Player");
	// }
	// void FixedUpdate () {
	// 	float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
	// 	float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

	// 	transform.position = new Vector3 (posX, posY, transform.position.z);

	// 	if (bounds) {
	// 		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x), 
	// 			Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y), 
	// 			Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
	// 	}
	// }
	private Transform player;
	public float minX, maxX;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null){
			Vector3 temp = transform.position;
			temp.x = player.position.x;

			if(temp.x < minX){
				temp.x = minX;
			}

			if(temp.x > maxX){
				temp.x = maxX;
			}

			transform.position = temp;
		}
	}
}