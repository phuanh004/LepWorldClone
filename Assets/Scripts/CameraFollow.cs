using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform lookAt;
	public float minX, maxX;
	public float minY, maxY;

	// Use this for initialization
	void Start () {
	
	}

	void LateUpdate(){
		Vector3 temp = transform.position;
		temp.x = lookAt.position.x;
		temp.y = lookAt.position.y;

		if(temp.x < minX){
			temp.x = minX;
		}

		if(temp.x > maxX){
			temp.x = maxX;
		}

		if(temp.y < minY){
			temp.y = minY;
		}

		if(temp.y > maxY){
			temp.y = maxY;
		}

		transform.position = temp;
	}
}
