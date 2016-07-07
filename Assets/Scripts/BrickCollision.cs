using UnityEngine;
using System.Collections;

public class BrickCollision : MonoBehaviour {

	private AudioSource audioS;

	void Start(){
		audioS = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			audioS.Play ();
			Destroy (this.gameObject, .26f);
		}
	}
}
