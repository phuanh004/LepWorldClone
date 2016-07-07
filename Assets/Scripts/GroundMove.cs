using UnityEngine;
using System.Collections;

public class GroundMove : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Player")) {
			this.GetComponent<EnemyMove> ().enabled = true;
		}
	}
}
